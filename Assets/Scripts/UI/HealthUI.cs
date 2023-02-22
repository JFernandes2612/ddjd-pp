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
    // Start is called before the first frame update
    void Start()
    {
        healthBar = (ProgressBar)GetComponent<UIDocument>().rootVisualElement.Q("Health").Q("HealthBar");
        health = player.GetComponent<Health>();

        healthBar.highValue = health.getMaxHealth();
        healthBar.value = health.getCurrentHealth();
        healthBar.title =  healthBar.value + "/" + healthBar.highValue;
    }
}
