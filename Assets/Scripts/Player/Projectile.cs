using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float xBound = 50f;
    [SerializeField]
    private float zBound = 50f;

    // TODO: make abstract and override once we actually have some more guns and projectile types
    // protected abstract void DestroyProjectile();

    void Update()
    {
        CheckBounds();
    }

    // Verifies if the projectile is within the expected bounds. Destroys it if it leaves them
    void CheckBounds()
    {
        bool inXBound = transform.position.x < xBound && transform.position.x > -xBound;
        bool inZBound = transform.position.z < zBound && transform.position.z > -zBound;
        if (!(inXBound && inZBound))
        {
            DestroyProjectile();
        }
    }

    // Destroys this Proejectile
    protected void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
