using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    // Scene Object References
    protected EnemyController enemyController;
    [SerializeField]
    protected GameObject player;

    // Movement-related fields
    private Vector3 moveDirection;
    [SerializeField]
    protected float moveSpeed = 5f;

    // Abstract methods
    protected abstract Vector3 GetMoveDirection(); // TODO: abstract this and Player's GetMoveDirection method
    protected abstract void Die(); // TODO: abstract this and Player's Die method

    // Fetches reference to the player model on the first frame
    void Start()
    {
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

    // Moves the object instance
    void Move()
    {
        // TODO: abstract this and Player's Move method
        transform.position = transform.position + (moveDirection * moveSpeed * Time.deltaTime);
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
