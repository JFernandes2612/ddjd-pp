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
        canvas.GetComponent<CanvasGroup>().alpha = 0;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.LookAt(transform.position + cameraTransform.forward);
        if(PlayerInRadius()){
            canvas.SetActive(true);
            SetCanvasOpacity();
        }
        else{
            canvas.SetActive(false);
        }
    }

    private bool PlayerInRadius(){
        return Vector3.Distance(playerTransform.position, transform.position) <= radius;
    }

    private void SetCanvasOpacity(){
        CanvasGroup canvasGroup = canvas.GetComponent<CanvasGroup>();
        float opacityThreshold = 5.0f;
        float dist = Vector3.Distance(playerTransform.position, transform.position);
        if(dist < opacityThreshold){
            canvasGroup.alpha = 1;
        }
        else{
            float localDist = dist - opacityThreshold;
            canvasGroup.alpha = Mathf.Abs(localDist - 5) / Mathf.Abs((radius - opacityThreshold));
        }
    }
}
