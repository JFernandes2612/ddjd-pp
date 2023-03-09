using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponTester : MonoBehaviour
{
    private GameObject[] gunList;

    // Gets all weapons in the scene, and starts a testing coroutine
    void Start()
    {
        gunList = GameObject.FindGameObjectsWithTag("Weapon");
    }

    // Tries to shoot every weapon
    void Update(){
        for(int i = 0; i < gunList.Length; i++){
            gunList[i].GetComponent<Weapon>().Shoot();
        }
    }
}