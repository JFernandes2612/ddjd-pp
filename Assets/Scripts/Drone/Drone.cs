using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{
    private Rigidbody rb;
    public GameObject player;

    [SerializeField]
    private float moveSpeed = 5f;

    private Vector3 moveDirection;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        moveDirection = player.transform.position - transform.position;
        moveDirection.Normalize();
    }

    // Update is called once per frame
    void FixedUpdate()
    {   
        transform.position = transform.position + (moveDirection * moveSpeed * Time.deltaTime);
    }
}
