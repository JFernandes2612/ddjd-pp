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

    // Shooting-related fields
    [SerializeField]
    private float bulletSpeed = 100.0f;

    // Coins
    private int coins = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = GetMoveDirection();

        // shooting
        transform.rotation = Quaternion.identity;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            transform.LookAt(new Vector3(raycastHit.point.x, transform.position.y, raycastHit.point.z));
        }

        if (Input.GetMouseButtonDown(0))
        {
            GameObject bulletObject = Instantiate(bullet, shootPoint.transform.position, Quaternion.Euler(bullet.transform.rotation.eulerAngles + transform.rotation.eulerAngles));
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

    // Handles collisions between this and other object instances
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("EnemyProjectile"))
        {
            Projectile projScript = collision.gameObject.GetComponent<Projectile>();
            health -= projScript.getDamage();
            if (health <= 0) Die();
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemyScript = collision.gameObject.GetComponent<Enemy>();
            Damage(enemyScript.getDamage());
            if (health <= 0) Die();
        }
        else if (collision.gameObject.CompareTag("Coin"))
        {
            AddCoins(1);
        }
        else if (collision.gameObject.CompareTag("HealthOrb"))
        {
            Heal(1);
        }
        else if (collision.gameObject.CompareTag("Perk"))
        {
            Perk perk = collision.gameObject.GetComponent<Perk>();
            PerkType perkType = perk.getType();
            float quantity = perk.getQuantity();

            switch (perkType)
            {
                case PerkType.MaxHealth:
                    AddMaxHealth((int)quantity);
                    break;
                default:
                    break;
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.transform.parent.gameObject.CompareTag("Perk"))
        {
            Perk perk = other.gameObject.transform.parent.gameObject.GetComponent<Perk>();
            PerkType perkType = perk.getType();
            float quantity = perk.getQuantity();

            switch (perkType)
            {
                case PerkType.MaxHealth:
                    AddMaxHealth((int)quantity);
                    break;
                default:
                    break;
            }
        }
    }

    public int GetCoins()
    {
        return coins;
    }

    public bool haveEnoughCoins(int value) {
        return coins >= value;
    }

    public void AddCoins(int value)
    {
        coins += value;
    }

    public void RemoveCoins(int value)
    {
        coins -= value;
    }
}
