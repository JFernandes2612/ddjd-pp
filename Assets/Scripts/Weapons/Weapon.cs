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
    protected int damage;
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
    private float fireRate;
    bool canShoot = true;
    bool reloading = false;

    [SerializeField] //for debug
    private int bulletsInClip;

    // defines specific weapon bullet pattern spread
    protected abstract void InstantiateProjectiles();

    // setters for powerups
    public void SetDamage(int newDamage){
        damage = newDamage;
    }

    public void SetReloadSpeed(int newReloadSpeed){
        damage = newReloadSpeed;
    }

    public void SetFireRate(int newFireRate){
        damage = newFireRate;
    }

    // getters
    public int GetDamage(){
        return damage;
    }

    public int GetMagazineSize(){
        return magazineSize;
    }

    public float GetReloadSpeed(){
        return reloadSpeed;
    }

    public float GetBulletSpeed(){
        return bulletSpeed;
    }

    public float GetRange(){
        return range;
    }

    public float GetFireRate(){
        return fireRate;
    }

    // Start is called before the first frame update
    void Start()
    {
        shootPoint = transform.GetChild(0).GetComponent<Transform>();
        RefillClip();
    }

    // checks if shooting is possible, and creates the projectiles if it is
    // called from other scripts (player, tester, etc...)
    public void Shoot(){
        if(canShoot && !reloading){
            StartCoroutine(ShotCooldown());
            bulletsInClip--;
            InstantiateProjectiles();
            if(bulletsInClip == 0){
                StartCoroutine(Reload());
            }
        }
    }

    private void RefillClip(){
        bulletsInClip = magazineSize;
    }

    IEnumerator ShotCooldown(){
        canShoot = false;
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }

    IEnumerator Reload(){
        reloading = true;
        RefillClip();
        yield return new WaitForSeconds(reloadSpeed);
        reloading = false;
    }
}
