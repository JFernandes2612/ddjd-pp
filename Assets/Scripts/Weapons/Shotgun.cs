using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Weapon
{
    [SerializeField]
    private int pelletNumber = 3;

    protected override void InstantiateProjectiles(){
        for(int pellet = 0; pellet < pelletNumber; pellet++){
            float xRot = Random.Range(-45, 45);
            float yRot = Random.Range(-45, 45);
            Quaternion rotation = shootPoint.transform.rotation * bulletPrefab.transform.rotation * Quaternion.Euler(xRot, yRot, 0);
            GameObject spawnedProjectile = Instantiate(bulletPrefab, shootPoint.position, rotation);
            Projectile projectile = spawnedProjectile.GetComponent<Projectile>();
            projectile.SetRange(range);
            projectile.SetDamage(damage);
            spawnedProjectile.GetComponent<Rigidbody>().velocity = rotation * shootPoint.forward * bulletSpeed;
        }
    }
}
