using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PerkType
{
    MaxHealth
}

public class Perk : MonoBehaviour
{
    [SerializeField]
    private PerkType type;

    [SerializeField]
    private float quantity;

    public PerkType getType() {
        return type;
    }

    public float getQuantity() {
        return quantity;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player"))
            Destroy(gameObject);
    }
}
