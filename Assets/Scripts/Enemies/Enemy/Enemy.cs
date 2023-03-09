using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public abstract class Enemy : Entity
{
    // Scene Object References
    protected EnemyController enemyController;
    protected GameObject player;

    // damage text prefab
    [SerializeField]
    private GameObject damageTextPrefab;

    // enemy health bar prefab
    [SerializeField]
    private GameObject enemyCanvasPrefab;
    private GameObject enemyUI;
    private Slider healthBar;

    [SerializeField]
    protected int damage;
    protected float extraDamage;
    [SerializeField]
    protected int points;


    // Collectables drops
    [SerializeField]
    private GameObject[] collectables;

    [SerializeField]
    private float[] minCollectablesDrop;
    [SerializeField]
    private float[] maxCollectablesDrop;


    // Perks drops
    [SerializeField]
    private GameObject[] perks;
    [SerializeField]
    private float[] perksDropChances;

    private bool dead = false;

    // Fetches reference to the player model on the first frame
    protected override void Start()
    {
        base.Start();
        setBaseStats();
        // Assert same sizes
        Debug.Assert(collectables.Length == minCollectablesDrop.Length);
        Debug.Assert(collectables.Length == maxCollectablesDrop.Length);
        Debug.Assert(perks.Length == perksDropChances.Length);

        Debug.Assert(perksDropChances.Sum() <= 1.0f);

        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
        CreateEnemyUI();
    }

    // Sets a reference to the EnemyController script component of the Enemy Controller GameObject
    public void SetEnemyController(EnemyController enemyController)
    {
        this.enemyController = enemyController;
    }

    public override void Damage(int value) {
        base.Damage(value);
        GameObject text = Instantiate(damageTextPrefab, enemyUI.transform.position, enemyUI.transform.rotation, enemyUI.transform);
        float baseYOffset = -25f;
        float extraXOffset = Random.Range(-25f, 25f);
        float extraYOffset = Random.Range(-15f, 15f);
        Vector3 offset = new Vector3(extraXOffset, baseYOffset + extraYOffset, 0);
        text.transform.localPosition += offset; // offset relative to parent transform
        text.GetComponent<TextController>().SetElementText(value.ToString());
    }

    public void SetExtraDamage(float value) {
        extraDamage = value;
    }

    public float GetExtraDamage() {
        return extraDamage;
    }

    public int getDamage()
    {
        return (int)(damage * (1.0f + extraDamage));
    }

    private void CreateEnemyUI()
    {
        Vector3 pos = transform.position + canvasOffsets;
        Quaternion rot = Quaternion.Euler(75, 0, 0);
        enemyUI = Instantiate(enemyCanvasPrefab, pos, rot);
        enemyUI.transform.SetParent(transform, true);
        foreach(Transform child in enemyUI.transform){
            if(child.name == "EnemyHpBar"){
                healthBar = child.GetComponent<Slider>();
            }
        }
        SetSlider();
    }

    private void SetSlider()
    {
        healthBar.maxValue = health;
        healthBar.value = health;
    }

    private void UpdateSlider()
    {
        healthBar.value = health;
    }

    public int getPoints()
    {
        return points;
    }

    // Input gathering
    protected virtual void Update()
    {
        moveDirection = GetMoveDirection();
    }

    // Physics Calculations
    void FixedUpdate()
    {
        Move();
    }

    protected override void Die()
    {
        if (!dead) {
            dead = true;
            dropCollectables();

            dropPerks();

            enemyController.RemoveEnemy(gameObject);
        }
    }

    private void dropCollectables()
    {
        for (int i = 0; i < collectables.Length; i++)
        {
            int quantity = (int)Random.Range(minCollectablesDrop[i], maxCollectablesDrop[i]);
            for (int c = 0; c < quantity; c++)
                Instantiate(collectables[i], transform.position, Quaternion.identity);
        }
    }

    private void dropPerks()
    {
        float currentPerkDropChance = 0.0f;
        float drop = Random.Range(0.0f, 1.0f);
        for (int i = 0; i < perks.Length; i++)
        {
            currentPerkDropChance += perksDropChances[i];

            if (drop < currentPerkDropChance)
            {
                Instantiate(perks[i], new Vector3(transform.position.x, -0.575f, transform.position.z), Quaternion.identity);
                break;
            }
        }
    }

    void HandleCollision(GameObject projectile)
    {
        if (projectile.CompareTag("PlayerProjectile"))
        {
            Projectile projScript = projectile.GetComponent<Projectile>();
            Damage(projScript.getDamage());
            UpdateSlider();
            if (health <= 0) Die();
        }
    }

    // Handles collisions between this and other object instances
    void OnCollisionEnter(Collision collision)
    {
        HandleCollision(collision.gameObject);
    }

    void OnTriggerEnter(Collider collider)
    {
        HandleCollision(collider.gameObject);
    }
}
