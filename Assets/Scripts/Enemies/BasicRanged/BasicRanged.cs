using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicRanged : RangedEnemy
{
    bool movingRight = false;
    protected override void Start()
    {
        base.Start();
        StartCoroutine(ChangeHorizontalDirection());
    }

    IEnumerator ChangeHorizontalDirection(){
        while(true){
            float interval = Random.Range(1.0f, 2.0f);
            yield return new WaitForSeconds(interval);
            movingRight = !movingRight;
        }
    }

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
            int horizontalDir = (movingRight) ? 1 : -1;
            Vector3 perpendicularDir = Vector3.Cross(dir, Vector3.up) * horizontalDir;
            return perpendicularDir.normalized * 0.5f;
        }
    }



    protected override void rotateWeapon()
    {
        weaponObject.transform.LookAt(player.transform);
    }
}
