using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenetratingProjectile : Projectile
{
    protected override void OnCollisionEnter(Collision collision) {}

    private void OnTriggerEnter(Collider other) {
        if (!other.gameObject.CompareTag("Enemy")) {
            Destroy(gameObject);
        }
    }
}
