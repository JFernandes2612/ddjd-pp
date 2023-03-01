using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    // Movement-related fields
    protected Rigidbody rb;
    protected Vector3 moveDirection;
    [SerializeField]
    protected float moveSpeed;

    protected abstract Vector3 GetMoveDirection();
    protected abstract void Die();

    // Health-related fields
    [SerializeField]
    protected int maxHealth = 100;
    [SerializeField]
    protected int health = 50;

    // Moves the object instance
    protected void Move()
    {
        rb.velocity = moveDirection.normalized * moveSpeed;
    }

    public void Damage(int value) {
        health = Mathf.Max(health - value, 0);
    }

    public void Heal(int value) {
        health = Mathf.Min(health + value, maxHealth);
    }

    public int GetMaxHealth() {
        return maxHealth;
    }

    public void AddMaxHealth(int value) {
        maxHealth += value;
    }

    public int GetCurrentHealth() {
        return health;
    }
}
