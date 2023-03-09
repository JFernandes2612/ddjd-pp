using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : Enemy
{
    // Calculates and returns the player's input
    protected override Vector3 GetMoveDirection()
    {
        Vector3 dir = player.transform.position - transform.position;
        dir.Normalize();
        return dir;
    }
}
