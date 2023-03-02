using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    // Weapon related variables
    [SerializeField]
    private GameObject primaryWeapon;

    [SerializeField]
    #nullable restore
    private GameObject secondaryWeapon;

    // Coins
    private int coins = 0;

    //Perks
    private Dictionary<PerkType, int> perks = new Dictionary<PerkType, int>();

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        primaryWeapon = SpawnWeapon(primaryWeapon);
    }

    private GameObject SpawnWeapon(GameObject weapon) {
        GameObject weaponSpawned = Instantiate(weapon, transform.position + weapon.transform.localScale.z * transform.forward / 2 + transform.localScale.z * transform.forward / 2, transform.localRotation);
        weaponSpawned.transform.parent = transform;
        return weaponSpawned;
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

        if (Input.GetMouseButton(0))
        {
            primaryWeapon.GetComponent<Weapon>().Shoot();
        }

        if (Input.GetKeyDown("r")) {
            primaryWeapon.GetComponent<Weapon>().StartCoroutine("Reload");
        }

        if (Input.GetKeyDown("q")) {
            swapWeapon();
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
    }

    private void OnTriggerEnter(Collider other)
    {
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

            if (!perks.ContainsKey(perkType))
            {
                perks[perkType] = 0;
            }

            perks[perkType]++;
        }
    }

    public int GetCoins()
    {
        return coins;
    }

    public bool haveEnoughCoins(int value)
    {
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

    public Dictionary<PerkType, int> getPerks()
    {
        return perks;
    }

    public GameObject getPrimaryWeapon() {
        return primaryWeapon;
    }

    private void swapWeapon() {
        if (secondaryWeapon) {
            GameObject temp = secondaryWeapon;

            secondaryWeapon = primaryWeapon;
            secondaryWeapon.GetComponent<MeshRenderer>().enabled = false;
            primaryWeapon = temp;

            if (!primaryWeapon.activeInHierarchy) {
                primaryWeapon = SpawnWeapon(temp);
            }
            primaryWeapon.GetComponent<MeshRenderer>().enabled = true;
        }
    }

    public void getNewWeapon(GameObject weapon) {
        if (secondaryWeapon) {
            Destroy(primaryWeapon);
            primaryWeapon = SpawnWeapon(weapon);
        } else {
            secondaryWeapon = SpawnWeapon(weapon);
            swapWeapon();
        }
    }
}
