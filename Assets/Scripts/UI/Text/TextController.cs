using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextController : MonoBehaviour
{
    [SerializeField]
    private float textTime = 3.0f;

    void Start(){
        Destroy(gameObject, textTime);
    }

    public void SetDamageText(string newText){
        GetComponent<TextMeshProUGUI>().SetText(newText);
    }
    
}
