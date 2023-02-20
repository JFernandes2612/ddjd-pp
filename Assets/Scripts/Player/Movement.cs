using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 15f;
    private Vector3 input;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputZ = Input.GetAxisRaw("Vertical");
        float inputY = 0;
        input = new Vector3(inputX, inputY, inputZ);
        input.Normalize();
    }

    void FixedUpdate()
    {
        transform.position = transform.position + (input * moveSpeed * Time.deltaTime);
    }
}
