using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HealthUI : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    private Health health;
    private ProgressBar healthBar;
    private float initialWidth;

    // Start is called before the first frame update
    void Start()
    {
        healthBar = (ProgressBar)GetComponent<UIDocument>().rootVisualElement.Q("Health").Q("HealthBar");
        initialWidth = 250.0f;
        health = player.GetComponent<Health>();
    }

    void FixedUpdate() {
        healthBar.highValue = health.getMaxHealth();
        healthBar.style.width = new StyleLength(Mathf.Min(initialWidth + healthBar.highValue * 0.05f, initialWidth*2));
        healthBar.value = health.getCurrentHealth();
        healthBar.title =  healthBar.value + "/" + healthBar.highValue;
    }
}
