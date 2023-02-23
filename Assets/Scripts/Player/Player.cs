using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Scene Object References
    [SerializeField]
    private GameObject shootPoint;
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private GameObject playerModel;

    // Movement-related fields
    private Vector3 input;
    [SerializeField]
    private float moveSpeed = 15f;

    // Shooting-related fields
    [SerializeField]
    private float bulletSpeed = 100.0f;

    // Update is called once per frame
    void Update()
    {
        input = GetMoveDirection();

        // shooting
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

    // Physics Calculations
    void FixedUpdate()
    {
        Move();
    }

    // Calculates and returns the player's input
    Vector3 GetMoveDirection()
    {
        // TODO: abstract this and Enemy's GetMoveDirection method
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputZ = Input.GetAxisRaw("Vertical");
        float inputY = 0;
        Vector3 inputVal = new Vector3(inputX, inputY, inputZ);
        inputVal.Normalize();
        return inputVal;
    }

    // Moves the object instance
    void Move()
    {
        // TODO: abstract this and Enemy's Move method
        transform.position = transform.position + (input * moveSpeed * Time.deltaTime);
    }

    // Destroys this GameObject instance
    void Die()
    {
        // TODO: abstract this and Player's Die method
        Destroy(gameObject);
    }
}
