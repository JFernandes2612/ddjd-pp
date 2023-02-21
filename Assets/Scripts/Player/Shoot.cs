using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    // Scene Object References
    [SerializeField]
    private GameObject shootPoint;
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private GameObject playerModel;

    // Shooting-related fields
    [SerializeField]
    private float bulletSpeed = 100.0f;

    // Update is called once per frame
    void Update()
    {
        playerModel.transform.rotation = Quaternion.identity;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            playerModel.transform.LookAt(new Vector3(raycastHit.point.x, playerModel.transform.position.y, raycastHit.point.z));
        }

        if (Input.GetMouseButtonDown(0))
        {
            GameObject bulletObject = Instantiate(bullet, shootPoint.transform.position, Quaternion.Euler(bullet.transform.rotation.eulerAngles + playerModel.transform.rotation.eulerAngles));
            bulletObject.GetComponent<Rigidbody>().AddRelativeForce(-bulletObject.transform.forward * bulletSpeed);
        }
    }

}
