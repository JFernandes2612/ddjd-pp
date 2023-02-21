using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : Enemy
{
    protected override Vector3 GetMoveDirection()
    {
        Vector3 dir = player.transform.position - transform.position;
        dir.Normalize();
        return dir;
    }

    protected override void Die()
    {
        enemyController.RemoveEnemy(gameObject);
    }
}
