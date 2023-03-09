using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDrop : MonoBehaviour
{
    private GameObject player;

    [SerializeField]
    private string weaponName;

    [SerializeField]
    private float minCatchRadius = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        InitDropDescription();
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

    void InitDropDescription(){
        foreach(Transform child in transform){
            // find popup child
            if(child.gameObject.CompareTag("popup ui")){
                Transform canvasTransform = child.GetChild(0);
                foreach(Transform canvasElement in canvasTransform){
                    // find description child
                    if(canvasElement.gameObject.CompareTag("DescriptionUI")){
                        canvasElement.GetComponent<TextController>().SetElementText(weaponName);
                        break;
                    }
                }
                break;
            }
        }
    }
}
