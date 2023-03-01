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

    // Perks
    private VisualElement perksVisualElement;

    // Start is called before the first frame update
    void Start()
    {

        numberOfCoinsLabel = (Label)GetComponent<UIDocument>().rootVisualElement.Q("Coins").Q("CoinsValue");
        healthBar = (ProgressBar)GetComponent<UIDocument>().rootVisualElement.Q("Health").Q("HealthBar");
        perksVisualElement = (VisualElement)GetComponent<UIDocument>().rootVisualElement.Q("Perks");
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

    void updatePerks() {
        Dictionary<PerkType, int> perks = player.getPerks();

        foreach (KeyValuePair<PerkType, int> de in perks) {
            VisualElement perkVisualElement = perksVisualElement.Q(de.Key.ToString());
            if (perkVisualElement == null) {

                perkVisualElement = new VisualElement();
                perkVisualElement.name = de.Key.ToString();
                perkVisualElement.Add(new Label(string.Format("<b>{0}:</b> 1", de.Key)));

                perksVisualElement.Add(perkVisualElement);
            }
            else
            {
                Label text = (Label)perkVisualElement.ElementAt(0);
                text.text = string.Format("<b>{0}:</b> {1}",de.Key, de.Value);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        updateHealthBar();
        updateNumberOfCoinsLabel();
        updatePerks();
    }
}
