using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : Enemy
{
    protected override void SetFields(){
        moveSpeed = 5f;
        hp = 5;
    }
    // Calculates and returns the player's input
    protected override Vector3 GetMoveDirection()
    {
        // TODO: abstract this and Player's GetMoveDirection method
        Vector3 dir = player.transform.position - transform.position;
        dir.Normalize();
        return dir;
    }
}
