using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
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

    // Coins
    private int coins = 0;

    void Start()
    {
        moveSpeed = 15f;
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = GetMoveDirection();

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
    protected override Vector3 GetMoveDirection()
    {
        // TODO: abstract this and Enemy's GetMoveDirection method
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputZ = Input.GetAxisRaw("Vertical");
        float inputY = 0;
        Vector3 inputVal = new Vector3(inputX, inputY, inputZ);
        inputVal.Normalize();
        return inputVal;
    }

    // Destroys this GameObject instance
    protected override void Die()
    {
        // TODO: abstract this and Player's Die method
        Destroy(gameObject);
    }

    public int getCoins() {
        return coins;
    }

    public void addCoins(int coinsToAdd) {
        coins += coinsToAdd;
    }

    public void removeCoins(int coinsToRemove) {
        coins -= coinsToRemove;
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "HealthOrb") {
            //! HEAL 1 HP NICE POGGERS
        }
    }
}
