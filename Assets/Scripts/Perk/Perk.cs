using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PerkType
{
    MaxHealth,
    MoveSpeed,
    Damage
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

    private void Start() {
        transform.parent = GameObject.FindGameObjectWithTag("PerksEmpty").transform;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player"))
            Destroy(gameObject);
    }
}
