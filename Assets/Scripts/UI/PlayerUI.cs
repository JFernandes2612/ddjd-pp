using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    private Player player;

    // Health
    private ProgressBar healthBar;
    private float healthBarInitialWidth = 250.0f;

    // Coins
    private Label numberOfCoinsLabel;

    // Start is called before the first frame update
    void Start()
    {

        numberOfCoinsLabel = (Label)GetComponent<UIDocument>().rootVisualElement.Q("Coins").Q("CoinsValue");
        healthBar = (ProgressBar)GetComponent<UIDocument>().rootVisualElement.Q("Health").Q("HealthBar");
    }

    void updateHealthBar() {
        healthBar.highValue = player.GetMaxHealth();
        healthBar.style.width = new StyleLength(Mathf.Min(healthBarInitialWidth + healthBar.highValue * 0.05f, healthBarInitialWidth*2));
        healthBar.value = player.GetCurrentHealth();
        healthBar.title =  healthBar.value + "/" + healthBar.highValue;
    }

    void updateNumberOfCoinsLabel() {
        numberOfCoinsLabel.text = player.GetCoins().ToString();
    }

    // Update is called once per frame
    void Update()
    {
        updateHealthBar();
        updateNumberOfCoinsLabel();
    }
}
