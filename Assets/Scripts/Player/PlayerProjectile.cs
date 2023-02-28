using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : Projectile
{
    // Handles collisions between this and other object instances
    void OnCollisionEnter(Collision collision)
    {
        // Collision with wall or door
        if (collision.gameObject.layer == 7 || collision.gameObject.layer == 8){
            DestroyProjectile();
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            DestroyProjectile();
        }
    }
}
