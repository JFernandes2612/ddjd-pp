using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Movement-related fields
    private Vector3 input;
    [SerializeField]
    private float moveSpeed = 15f;

    // Input gathering
    void Update()
    {
        input = GetMoveDirection();
    }

    // Calculates and returns the player's input
    Vector3 GetMoveDirection()
    {
        // TODO: abstract this and Enemy's GetMoveDirection method
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputZ = Input.GetAxisRaw("Vertical");
        float inputY = 0;
        Vector3 inputVal = new Vector3(inputX, inputY, inputZ);
        inputVal.Normalize();
        return inputVal;
    }

    // Physics Calculations
    void FixedUpdate()
    {
        Move();
    }

    // Moves the object instance
    void Move()
    {
        // TODO: abstract this and Enemy's Move method
        transform.position = transform.position + (input * moveSpeed * Time.deltaTime);
    }

    // Destroys this GameObject instance
    void Die()
    {
        // TODO: abstract this and Player's Die method
        Destroy(gameObject);
    }

}
