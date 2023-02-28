using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    // Movement-related fields
    protected Vector3 moveDirection;
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
        // TODO: abstract this and Player's Move method
        transform.position = transform.position + (moveDirection * moveSpeed * Time.deltaTime);
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

    public int GetCurrentHealth() {
        return health;
    }
}
