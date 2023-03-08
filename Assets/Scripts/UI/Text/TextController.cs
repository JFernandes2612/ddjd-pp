using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextController : MonoBehaviour
{
    [SerializeField]
    private float textTime = 3.0f; // 0 means don't destroy

    void Start(){
        if(textTime != 0)
            Destroy(gameObject, textTime);
    }

    public void SetElementText(string newText){
        GetComponent<TextMeshProUGUI>().SetText(newText);
    }
    
}
