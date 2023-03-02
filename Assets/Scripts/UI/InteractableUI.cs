using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableUI : MonoBehaviour
{
    private Transform cameraTransform;
    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.LookAt(transform.position + cameraTransform.forward);
    }
}
