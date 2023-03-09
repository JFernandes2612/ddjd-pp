using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField]
    protected GameObject bulletPrefab;
    protected Transform shootPoint;

    // fields the specific weapon needs to know about
    [SerializeField]
    protected int baseDamage;
    private float extraDamage = 0.0f;
    [SerializeField]
    protected float bulletSpeed;
    [SerializeField]
    protected float range;


    // fields necessary to check if we can shoot
    [SerializeField]
    private int magazineSize;
    [SerializeField]
    private float reloadSpeed;
    [SerializeField]
    private float baseFireRate;
    private float extraFireRate = 0.0f;
    private bool canShoot = true;
    private bool reloading = false;

    AudioSource audioSource;
    [SerializeField]
    AudioClip shootAudio;

    private int bulletsInMagazine;


    [SerializeField]
    private Texture2D fireCursor;

    [SerializeField]
    private Texture2D reloadCursor;

    private Coroutine reloadingCoroutine;

    public void SetExtraDamage(float value) {
        extraDamage = value;
    }

    public void setExtraFireRate(float value) {
        extraFireRate = value;
    }

    // getters
    public int GetDamage()
    {
        return (int)((float)baseDamage * (1.0f + extraDamage));
    }

    public int GetMagazineSize()
    {
        return magazineSize;
    }

    public float GetReloadSpeed()
    {
        return reloadSpeed;
    }

    public float GetBulletSpeed()
    {
        return bulletSpeed;
    }

    public float GetRange()
    {
        return range;
    }

    public float GetFireRate()
    {
        return baseFireRate * (1.0f + extraFireRate);
    }

    public int getBulletsInMagazine()
    {
        return bulletsInMagazine;
    }

    // Start is called before the first frame update
    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
        foreach(Transform child in transform){
            if(child.gameObject.CompareTag("ShootPoint")){
                shootPoint = child;
                break;
            }
        }
        RefillClip();
    }

    // defines specific weapon bullet pattern spread
    protected abstract void InstantiateProjectiles();

    public void CheckMagazine() {
        if (bulletsInMagazine <= 0)
        {
            ReloadF();
        }
    }

    public void ReloadF() {
        reloadingCoroutine = StartCoroutine(Reload());
    }

    // checks if shooting is possible, and creates the projectiles if it is
    // called from other scripts (player, tester, etc...)
    public void Shoot()
    {
        if (canShoot && !reloading)
        {
            StartCoroutine(ShotCooldown());
            bulletsInMagazine--;
            InstantiateProjectiles();
            audioSource.PlayOneShot(shootAudio);
            CheckMagazine();
        }
    }

    public void RefillClip()
    {
        bulletsInMagazine = magazineSize;
    }

    IEnumerator ShotCooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(1 / GetFireRate());
        canShoot = true;
    }

    IEnumerator Reload()
    {
        reloading = true;
        Cursor.SetCursor(reloadCursor, Vector2.zero, CursorMode.Auto);
        yield return new WaitForSeconds(reloadSpeed);
        RefillClip();
        reloading = false;
        Cursor.SetCursor(fireCursor, Vector2.zero, CursorMode.Auto);
    }

    public void StopReloading() {
        if (reloading) {
            StopCoroutine(reloadingCoroutine);
            Cursor.SetCursor(fireCursor, Vector2.zero, CursorMode.Auto);
            reloading = false;
        }
    }
}
