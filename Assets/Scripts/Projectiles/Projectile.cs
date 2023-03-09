using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Bound checking-related fields
    protected float range;
    protected Vector3 initPosition;

    protected int damage;

    private bool ready = false;

    public void SetRange(float newRange){
        range = newRange;
    }

    public void SetDamage(int newDamage) {
        damage = newDamage;
    }

    // Fetches reference to the projectiles Empty GameObject on the first frame and sets it to this Projectile instance's transform's parent
    protected void Start()
    {
        initPosition = transform.position;
        transform.parent = GameObject.FindGameObjectWithTag("ProjectileEmpty").transform;
    }

    protected void Update()
    {
        CheckBounds();
    }

    public void Set() {
        ready = true;
    }

    public int getDamage() {
        return damage;
    }

    // Verifies if the projectile is within the expected bounds. Destroys it if it leaves them
    protected void CheckBounds()
    {
        if (Vector3.Distance(transform.position, initPosition) >= range && ready)
        {
            Destroy(gameObject);
        }
    }

    // Destroys this Projectile
    protected virtual void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
