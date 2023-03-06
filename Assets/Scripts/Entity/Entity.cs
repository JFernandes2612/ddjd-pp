using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    // Movement-related fields
    protected Rigidbody rb;
    protected Vector3 moveDirection;
    [SerializeField]
    protected float baseMoveSpeed;
    protected float moveSpeed;

    protected abstract Vector3 GetMoveDirection();
    protected abstract void Die();

    // Health-related fields
    [SerializeField]
    protected int baseMaxHealth = 100;
    protected int maxHealth;
    protected int health;

    protected void setBaseStats() {
        moveSpeed = baseMoveSpeed;
        maxHealth = baseMaxHealth;
        health = maxHealth;
    }

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

    public void AddMovementSpeed(float value) {
        moveSpeed += value;
    }
}
