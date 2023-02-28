using System.Collections;
using System.Collections.Generic;
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

    protected int hp;
    private GameObject enemyUI;
    private Slider hpBar;

    protected abstract void SetFields();

    // Fetches reference to the player model on the first frame
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("PlayerModel");
        SetFields();
        CreateEnemyUI();
    }

    // Sets a reference to the EnemyController script component of the Enemy Controller GameObject
    public void SetEnemyController(EnemyController enemyController)
    {
        this.enemyController = enemyController;
    }

    private void CreateEnemyUI(){
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z + 0.7f);
        Quaternion rot = Quaternion.Euler(75, 0, 0);
        enemyUI = Instantiate(enemyCanvasPrefab, pos, rot);
        enemyUI.transform.parent = transform;
        hpBar = enemyUI.transform.GetChild(0).GetComponent<Slider>();
        SetMaxHealth();
    }

    private void SetMaxHealth(){
        hpBar.maxValue = hp;
        hpBar.value = hp;
    }

    private void UpdateHealth(){
        hpBar.value = hp;
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

    // Handles collisions between this and other object instances
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlayerProjectile"))
        {
            hp--;
            UpdateHealth();
            if(hp == 0) Die();
        }
    }
}
