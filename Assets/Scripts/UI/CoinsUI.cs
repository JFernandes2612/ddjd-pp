using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CoinsUI : MonoBehaviour
{
    [SerializeField]
    private Player playerData;
    private Label numberOfCoinsUI;

    // Start is called before the first frame update
    void Start()
    {
        numberOfCoinsUI = (Label)GetComponent<UIDocument>().rootVisualElement.Q("Coins").Q("CoinsValue");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        numberOfCoinsUI.text = playerData.getCoins().ToString();
    }
}
