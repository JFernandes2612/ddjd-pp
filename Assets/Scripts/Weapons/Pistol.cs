using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon
{
    protected override void InstantiateProjectiles(){
        GameObject spawnedProjectile = Instantiate(bulletPrefab, shootPoint.position, shootPoint.transform.rotation * bulletPrefab.transform.rotation);
        spawnedProjectile.GetComponent<Projectile>().SetRange(range);
        spawnedProjectile.GetComponent<Rigidbody>().velocity = shootPoint.forward * bulletSpeed;
    }
}
