using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform playerTransform;

    [SerializeField]
    private Vector3 playerOffset;
    [SerializeField]
    private Vector3 relativeRotation;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        transform.rotation = Quaternion.Euler(relativeRotation);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = playerTransform.position + playerOffset;
    }
}
