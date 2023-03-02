using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableUI : MonoBehaviour
{
    private Transform cameraTransform;
    private Transform playerTransform;
    private GameObject canvas;
    [SerializeField]
    private float radius;
    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        canvas = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.LookAt(transform.position + cameraTransform.forward);
        canvas.SetActive(PlayerInRadius());
    }

    private bool PlayerInRadius(){
        return Mathf.Pow((playerTransform.position.x - transform.position.x), 2) + Mathf.Pow((playerTransform.position.z - transform.position.z), 2) <= Mathf.Pow(radius, 2);
    }
}
