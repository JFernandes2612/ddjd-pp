using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    private float extraDamage = 0.0f;
    private float extraFireRate = 0.0f;

    private bool inMainArena = true;

    private bool immune = false;

    [SerializeField]
    private float immunityTime = 1.0f;

    // damage text prefab
    [SerializeField]
    private GameObject damageTextPrefab;
    private GameObject playerUI;

    protected void Start()
    {
        setBaseStats();
        rb = GetComponent<Rigidbody>();
        primaryWeapon = SpawnWeapon(primaryWeapon);
        foreach(Transform child in transform){
            if(child.tag == "popup ui"){
                playerUI = child.gameObject;
                break;
            }
        }
    }

    private GameObject SpawnWeapon(GameObject weapon) {
        GameObject weaponSpawned = Instantiate(weapon, transform.position + weapon.transform.localScale.z * transform.forward / 2 + transform.localScale.z * transform.forward / 2, transform.localRotation);
        weaponSpawned.GetComponent<Weapon>().Start();
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
            primaryWeapon.GetComponent<Weapon>().SetExtraDamage(extraDamage);
            primaryWeapon.GetComponent<Weapon>().setExtraFireRate(extraFireRate);
            primaryWeapon.GetComponent<Weapon>().Shoot();
        }

        if (Input.GetKeyDown("r")) {
            primaryWeapon.GetComponent<Weapon>().ReloadF();
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
        StartCoroutine(LoadMainMenuAsyncScene());
    }

    private IEnumerator LoadMainMenuAsyncScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(0);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    public override void Damage(int value) {
        base.Damage(value);
        GameObject text = Instantiate(damageTextPrefab, playerUI.transform.position, playerUI.transform.rotation, playerUI.transform);
        float baseYOffset = -25f;
        float extraXOffset = Random.Range(-25f, 25f);
        float extraYOffset = Random.Range(-15f, 15f);
        Vector3 offset = new Vector3(extraXOffset, baseYOffset + extraYOffset, 0);
        text.transform.localPosition += offset; // offset relative to parent transform
        text.GetComponent<TextController>().SetElementText(value.ToString());
    }

    // Handles collisions between this and other object instances
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("EnemyProjectile") && !immune)
        {
            Projectile projScript = collision.gameObject.GetComponent<Projectile>();
            Damage(projScript.getDamage());
            if (health <= 0) Die();
            StartCoroutine(ImmunityCoroutine());
        }
        else if (collision.gameObject.CompareTag("Enemy") && !immune)
        {
            Enemy enemyScript = collision.gameObject.GetComponent<Enemy>();
            Damage(enemyScript.getDamage());
            if (health <= 0) Die();
            StartCoroutine(ImmunityCoroutine());
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

    private void OnCollisionStay(Collision other) {
        if (other.gameObject.CompareTag("MainArena")) {
            inMainArena = true;
        } else {
            inMainArena = false;
        }
    }

    public bool getInMainArena() {
        return inMainArena;
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
                    AddMaxHealth((int)(quantity * baseMaxHealth));
                    break;
                case PerkType.MoveSpeed:
                    AddMovementSpeed(quantity * baseMoveSpeed);
                    break;
                case PerkType.Damage:
                    extraDamage += quantity;
                    break;
                case PerkType.FireRate:
                    extraFireRate += quantity;
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
            primaryWeapon.GetComponent<Weapon>().StopReloading();
            GameObject temp = secondaryWeapon;

            secondaryWeapon = primaryWeapon;
            DisableWeaponModelMeshes(secondaryWeapon);
            primaryWeapon = temp;

            if (!primaryWeapon.activeInHierarchy) {
                primaryWeapon = SpawnWeapon(temp);
                primaryWeapon.GetComponent<Weapon>().RefillClip();
            }
            EnableWeaponModelMeshes(primaryWeapon);
            primaryWeapon.GetComponent<Weapon>().CheckMagazine();
        }
    }

    private void EnableWeaponModelMeshes(GameObject weapon){
        MeshRenderer[] childMeshRenderers = weapon.GetComponentsInChildren<MeshRenderer>();
        foreach(MeshRenderer renderer in childMeshRenderers){
            renderer.enabled = true;
        }
    }

    private void DisableWeaponModelMeshes(GameObject weapon){
        MeshRenderer[] childMeshRenderers = weapon.GetComponentsInChildren<MeshRenderer>();
        foreach(MeshRenderer renderer in childMeshRenderers){
            renderer.enabled = false;
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

    IEnumerator ImmunityCoroutine() {
        immune = true;
        yield return new WaitForSeconds(immunityTime);
        immune = false;
    }
}
