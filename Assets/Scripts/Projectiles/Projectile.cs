using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Bound checking-related fields
    private float range;
    private Vector3 initPosition;

    protected int damage;

    public void SetRange(float newRange){
        range = newRange;
    }

    public void SetDamage(int newDamage) {
        damage = newDamage;
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
        if (Vector3.Distance(transform.position, initPosition) >= range)
        {
            Destroy(gameObject);
        }
    }

    // Destroys this Projectile
    void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
