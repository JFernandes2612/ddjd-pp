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

    // Moves the object instance
    protected void Move()
    {
        // TODO: abstract this and Player's Move method
        transform.position = transform.position + (moveDirection * moveSpeed * Time.deltaTime);
    }
}
