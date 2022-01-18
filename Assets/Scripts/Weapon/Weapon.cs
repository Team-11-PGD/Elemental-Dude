using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponTypes weaponType;
    public Camera playerCam;

    [SerializeField]
    private LayerMask layerMask;

    [Header("Bullet")]
    public Transform spawnBulletPos;
    public Transform bulletPrefab;
    public int curBulletAmount;
    public int maxBullets;
    public float maxBulletSpread = 0.02f;
    public float bulletSpeed = 40;
    public float weaponDamage = 1;
    public float extraDamage = 0, extraSpeed = 0;

    [Header("Timers")]
    public float reloadTime;
    public float fireInterval;

    private bool canFire = true;
    private bool isReloading;
    private float timeToFire;
    private float reloadEndTime;
    private Vector3 aimPoint;

    public GameObject currentTarget;

    [HideInInspector]
    public ElementMain elementMain;

    public enum WeaponTypes
    {
        Rifle,
        Shotgun,
        RPG
    }

    void Start()
    {
        elementMain = GetComponent<ElementMain>();
        curBulletAmount = maxBullets;
    }

    void Update()
    {
        Debug.DrawRay(playerCam.transform.position, playerCam.transform.forward * 50, Color.green);

        if (Input.GetButton("Fire1") && Time.time >= timeToFire && canFire)
        {
            //SOUND: (shoot)
            timeToFire = Time.time + fireInterval;
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.R) && !isReloading)
        {
            //SOUND: (reload)
            Reload();
        }

        if (isReloading && Time.time >= reloadEndTime)
        {
            canFire = true;
            isReloading = false;
            curBulletAmount = maxBullets;

            //SOUND: (done reloading sound)
            Debug.Log("Done reloading!");
        }
    }

    public void SetWeaponElement(ElementMain.ElementType elementType)
    {
        switch (elementType)
        {
            case ElementMain.ElementType.None:
                elementMain.currentType = ElementMain.ElementType.None;
                Debug.Log("I am a None element now");
                //SOUND: (no element)
                break;

            case ElementMain.ElementType.Water:
                elementMain.currentType = ElementMain.ElementType.Water;
                Debug.Log("I am a Water element now");
                //SOUND: (Water element)
                break;

            case ElementMain.ElementType.Fire:
                elementMain.currentType = ElementMain.ElementType.Fire;
                Debug.Log("I am a Fire element now");
                //SOUND: (fire element)
                break;

            case ElementMain.ElementType.Air:
                elementMain.currentType = ElementMain.ElementType.Air;
                Debug.Log("I am an Air element now");
                //SOUND: (air element)
                break;

            //case ElementMain.ElementType.Earth:
            //    elementMain.currentType = ElementMain.ElementType.Earth;
            //    Debug.Log("I am an Earth element now");
            //    //SOUND: (eart element)
            //    break;
        }
    }

    public void Shoot()
    {
        //AudioManager.instance.PlaySoundEffect(this.gameObject, "PewPew");

        if (curBulletAmount <= 0)
        {
            //SOUND: (reload)
            Reload();
            return;
        }
        else
        {
            curBulletAmount -= 1;

            if (curBulletAmount <= 0)
            {
                //SOUND: (reload)
                Reload();
                Debug.Log("Reloading...");
                canFire = false;
            }
        }

        RaycastHit hit;
        if (Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hit, float.MaxValue, layerMask))
        {
            aimPoint = hit.point;
        }
        Vector3 aimDir = (aimPoint - spawnBulletPos.position).normalized;

        Transform bullet;
        if (weaponType == WeaponTypes.Shotgun)
        {
            for (int i = 0; i < 6; i++)
            {
                bullet = Instantiate(bulletPrefab, spawnBulletPos.position, Quaternion.LookRotation(aimDir, Vector3.up));
                BulletProjectile bulletProjectile = bullet.GetComponent<BulletProjectile>();
                bulletProjectile.SetVelocity((bullet.forward + new Vector3(Random.Range(-maxBulletSpread, maxBulletSpread), Random.Range(-maxBulletSpread, maxBulletSpread), Random.Range(-maxBulletSpread, maxBulletSpread))) * bulletSpeed);
                bulletProjectile.SetElementType(elementMain.currentType);
                bulletProjectile.SetDamage(weaponDamage + extraDamage);
            }
        }
        else
        {
            bullet = Instantiate(bulletPrefab, spawnBulletPos.position, Quaternion.LookRotation(aimDir, Vector3.up));
            BulletProjectile bulletProjectile = bullet.GetComponent<BulletProjectile>();
            bulletProjectile.SetVelocity(bullet.forward * bulletSpeed);
            bulletProjectile.SetElementType(elementMain.currentType);
            bulletProjectile.SetDamage(weaponDamage + extraDamage);
        }
    }

    public virtual void Reload()
    {
        isReloading = true;
        canFire = false;
        reloadEndTime = Time.time + reloadTime - extraSpeed;
    }
}
