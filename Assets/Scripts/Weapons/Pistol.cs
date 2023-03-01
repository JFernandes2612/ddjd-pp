using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon
{
    protected override void InstantiateProjectiles(){
        GameObject spawnedProjectile = Instantiate(bulletPrefab, shootPoint.position, bulletPrefab.transform.rotation);
        spawnedProjectile.GetComponent<Rigidbody>().velocity = shootPoint.forward * bulletSpeed;
    }
}
