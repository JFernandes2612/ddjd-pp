using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    // Scene Object References
    private Transform projectilesEmpty;

    // Bound checking-related fields
    [SerializeField]
    private float xBound = 50f;
    [SerializeField]
    private float zBound = 50f;

    // damage dealt by contact with this projectile
    protected int damage;

    protected abstract void SetFields();

    // Fetches reference to the projectiles Empty GameObject on the first frame and sets it to this Projectile instance's transform's parent
    void Start()
    {
        projectilesEmpty = GameObject.FindGameObjectWithTag("ProjectileEmpty").transform;
        transform.parent = projectilesEmpty;
        SetFields();
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

    // Destroys this Proejectile
    protected void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
