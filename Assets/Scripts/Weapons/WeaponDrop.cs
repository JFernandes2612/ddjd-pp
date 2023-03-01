using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDrop : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private float minCatchRadius = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.transform.position, transform.position) <= minCatchRadius) {
            if (Input.GetKeyDown("e")) {
                GameObject weapon = gameObject.transform.GetChild(1).gameObject;
                weapon.GetComponent<Animator>().enabled = false;

                player.GetComponent<Player>().getNewWeapon(weapon);

                Destroy(gameObject);
            }
        }
    }
}
