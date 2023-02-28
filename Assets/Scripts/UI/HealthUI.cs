using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HealthUI : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private Entity entity;
    private ProgressBar healthBar;
    // Start is called before the first frame update
    private float initialWidth;
    void Start()
    {
        healthBar = (ProgressBar)GetComponent<UIDocument>().rootVisualElement.Q("Health").Q("HealthBar");
        initialWidth = 250.0f;
        entity = player.GetComponent<Entity>();
    }

    void FixedUpdate() {
        healthBar.highValue = entity.GetMaxHealth();
        healthBar.style.width = new StyleLength(initialWidth + healthBar.highValue * 0.05f);
        healthBar.value = entity.GetCurrentHealth();
        healthBar.title =  healthBar.value + "/" + healthBar.highValue;
    }
}
