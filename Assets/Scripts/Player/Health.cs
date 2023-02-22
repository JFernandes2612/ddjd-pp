using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int maxHealth = 100;
    private int health = 50;

    public void damage(int value) {
        health = Mathf.Max(health - value, 0);
    }

    public void heal(int value) {
        health = Mathf.Min(health + value, maxHealth);
    }

    public int getMaxHealth() {
        return maxHealth;
    }

    public int getCurrentHealth() {
        return health;
    }
}
