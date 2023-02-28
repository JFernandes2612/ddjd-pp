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
    private Vector3 canvasOffsets = new Vector3(0, 1.0f, 0.7f);
    private GameObject enemyUI;
    private Slider healthBar;

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
            health--;
            UpdateSlider();
            if(health == 0) Die();
        }
    }
}
