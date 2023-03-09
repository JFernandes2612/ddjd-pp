using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicRanged : RangedEnemy
{    
    // Calculates and returns the player's input
    protected override Vector3 GetMoveDirection()
    {
        Vector3 dir = player.transform.position - transform.position;
        float distanceToPlayer = dir.magnitude;
        
        if(distanceToPlayer > (hoverDistance + graceRange)){
            return dir.normalized;
        }
        else if(distanceToPlayer < (hoverDistance - graceRange)){
            return -dir.normalized;
        }
        else{
            int horizontalDir = (Random.Range(-1.0f, 1.0f) > 0) ? 1 : -1;
            Vector3 perpendicularDir = Vector3.Cross(dir, Vector3.up) * horizontalDir;
            return perpendicularDir.normalized;
        }
    }

    protected override void rotateWeapon()
    {
        weaponObject.transform.LookAt(player.transform);
    }
}
