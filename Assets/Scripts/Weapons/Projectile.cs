using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    // Bound checking-related fields
    [SerializeField]
    private float xBound;
    [SerializeField]
    private float yBound;
    [SerializeField]
    private float zBound;

    [SerializeField]
    protected int damage;

    public void SetRange(float range){
        xBound = range;
        yBound = range;
        zBound = range;
    }

    // Fetches reference to the projectiles Empty GameObject on the first frame and sets it to this Projectile instance's transform's parent
    void Start()
    {
        transform.parent = GameObject.FindGameObjectWithTag("ProjectileEmpty").transform;
    }

    void Update()
    {
        CheckBounds();
    }

    public int getDamage() {
        return damage;
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

    // Destroys this Projectile
    protected void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
