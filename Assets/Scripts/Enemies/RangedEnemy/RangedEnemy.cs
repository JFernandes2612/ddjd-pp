using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RangedEnemy : Enemy
{
    [SerializeField]
    protected GameObject weaponObject;
    [SerializeField]
    protected float hoverDistance;
    [SerializeField]
    protected float graceRange = 1;
    
    protected abstract void rotateWeapon();

    protected override void Update(){
        base.Update();
        rotateWeapon();
        weaponObject.GetComponent<Weapon>().Shoot();
    }
}
