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

    // enemy health par prefab
    [SerializeField]
    private GameObject enemyCanvasPrefab;
    private Vector3 canvasOffsets = new Vector3(0, 1.0f, 0.7f);
    private GameObject enemyUI;
    private Slider healthBar;

    [SerializeField]
    protected int damage;
    [SerializeField]
    protected int points;


    // Collectables drops
    [SerializeField]
    private GameObject[] collectables;

    [SerializeField]
    private float[] collectablesDropChances;


    // Perks drops
    [SerializeField]
    private GameObject[] perks;
    [SerializeField]
    private float[] perksDropChances;

    // Fetches reference to the player model on the first frame
    void Start()
    {
        // Assert same sizes
        Debug.Assert(collectables.Length == collectablesDropChances.Length);
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

    public int getDamage() {
        return damage;
    }

    public Vector3 GetCanvasOffsets(){
        return canvasOffsets;
    }

    private void CreateEnemyUI(){
        Vector3 pos = transform.position + canvasOffsets;
        Quaternion rot = Quaternion.Euler(75, 0, 0);
        enemyUI = Instantiate(enemyCanvasPrefab, pos, rot);
        enemyUI.transform.SetParent(transform, true);
        healthBar = enemyUI.transform.GetChild(0).GetComponent<Slider>();
        SetSlider();
    }

    private void SetSlider(){
        healthBar.maxValue = health;
        healthBar.value = health;
    }

    private void UpdateSlider(){
        healthBar.value = health;
    }

    public int getPoints() {
        return points;
    }

    // Input gathering
    void Update()
    {
        moveDirection = GetMoveDirection();
    }

    // Physics Calculations
    void FixedUpdate()
    {
        Move();
    }

    protected override void Die() {
        for (int i = 0; i < collectables.Length; i++) {
            int quantity = (int)collectablesDropChances[i] + (Random.Range(0.0f, 1.0f) < collectablesDropChances[i] - (int)collectablesDropChances[i] ? 1 : 0);
            for (int c = 0; c < quantity; c++)
                Instantiate(collectables[i], transform.position, Quaternion.identity);
        }

        float currentPerkDropChance = 0.0f;
        float drop = Random.Range(0.0f, 1.0f);
        for (int i = 0; i < perks.Length; i++) {
            currentPerkDropChance += perksDropChances[i];

            if (drop < currentPerkDropChance) {
                Instantiate(perks[i], new Vector3(transform.position.x, -0.575f, transform.position.z), Quaternion.identity);
                break;
            }
        }

        enemyController.RemoveEnemy(gameObject);
    }

    // Handles collisions between this and other object instances
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlayerProjectile"))
        {
            Projectile projScript = collision.gameObject.GetComponent<Projectile>();
            Damage(projScript.getDamage());
            UpdateSlider();
            if(health <= 0) Die();
        }
    }
}
