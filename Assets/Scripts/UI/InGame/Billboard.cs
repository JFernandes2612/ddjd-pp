using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Transform cameraTransform;
    [SerializeField]
    private Transform entityTransform;
    private Vector3 canvasOffsets;

    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
        entityTransform = transform.parent;
        canvasOffsets = entityTransform.GetComponent<Enemy>().GetCanvasOffsets();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = entityTransform.position + canvasOffsets;
        transform.LookAt(transform.position + cameraTransform.forward);
    }
}
