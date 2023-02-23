using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : Entity
{
    // Scene Object References
    protected EnemyController enemyController;
    protected GameObject player;

    // Fetches reference to the player model on the first frame
    void Start()
    {
        moveSpeed = 5f;
        player = GameObject.FindGameObjectWithTag("PlayerModel");
    }

    // Sets a reference to the EnemyController script component of the Enemy Controller GameObject
    public void SetEnemyController(EnemyController enemyController)
    {
        this.enemyController = enemyController;
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
            Die();
        }
    }
}
