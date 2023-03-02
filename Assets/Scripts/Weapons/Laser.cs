using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : Weapon
{
    protected override void InstantiateProjectiles(){
        GameObject spawnedProjectile = Instantiate(bulletPrefab, shootPoint.position, shootPoint.transform.rotation * bulletPrefab.transform.rotation);
        Projectile projectile = spawnedProjectile.GetComponent<Projectile>();
        projectile.SetRange(range);
        projectile.SetDamage(damage);
        spawnedProjectile.GetComponent<Rigidbody>().velocity = shootPoint.forward * bulletSpeed;
    }
}
