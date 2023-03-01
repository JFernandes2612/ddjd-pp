using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    // Bound checking-related fields
    [SerializeField]
    private float range;
    [SerializeField]
    private Vector3 initPosition;

    [SerializeField]
    protected int damage;

    public void SetRange(float newRange){
        range = newRange;
    }

    // Fetches reference to the projectiles Empty GameObject on the first frame and sets it to this Projectile instance's transform's parent
    void Start()
    {
        initPosition = transform.position;
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
        bool inXBound = transform.position.x < (initPosition.x + range) && transform.position.x > (initPosition.x - range);
        bool inYBound = transform.position.y < (initPosition.y + range) && transform.position.y > (initPosition.y - range);
        bool inZBound = transform.position.z < (initPosition.z + range) && transform.position.z > (initPosition.z - range);
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
