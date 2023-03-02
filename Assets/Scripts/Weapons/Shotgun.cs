using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Weapon
{
    [SerializeField]
    private int pelletNumber = 3;

    private float maxRadius = 1.0f;

    protected override void InstantiateProjectiles(){
        for(int pellet = 0; pellet < pelletNumber; pellet++){
            float rightRot = Random.Range(0, 180);
            float upRot = Random.Range(0, 180);
            Vector3 direction = (shootPoint.forward + Mathf.Cos(rightRot) * shootPoint.right / bulletSpeed * Random.Range(0.0f, maxRadius) + Mathf.Cos(upRot) * shootPoint.up / bulletSpeed * Random.Range(0.0f, maxRadius)).normalized;
            GameObject spawnedProjectile = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
            Projectile projectile = spawnedProjectile.GetComponent<Projectile>();
            projectile.SetRange(range);
            projectile.SetDamage(damage);
            spawnedProjectile.GetComponent<Rigidbody>().velocity = direction * bulletSpeed;
        }
    }
}
