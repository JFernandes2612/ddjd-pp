using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    // Scene Object References
    protected EnemyController enemyController;
    [SerializeField]
    protected GameObject player;

    // Movement
    private Vector3 moveDirection;
    [SerializeField]
    protected float moveSpeed = 5f;

    protected abstract Vector3 GetMoveDirection();
    protected abstract void Die();
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("PlayerModel");
    }
    public void SetEnemyController(EnemyController enemyController)
    {
        this.enemyController = enemyController;
    }

    void Update()
    {
        moveDirection = GetMoveDirection();
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        transform.position = transform.position + (moveDirection * moveSpeed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlayerProjectile"))
        {
            Die();
        }
    }
}
