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
    public float bulletDamage;

    [Header("Timers")]
    public float reloadTime;
    public float fireInterval;

    private bool canFire = true;
    private bool isReloading;
    private float timeToFire;
    private float reloadEndTime;
    private Vector3 aimPoint;

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
               
                break;

            case ElementMain.ElementType.Fire:
                elementMain.currentType = ElementMain.ElementType.Fire;
                Debug.Log("I am a Fire element now");
               
                break;

            case ElementMain.ElementType.Air:
                elementMain.currentType = ElementMain.ElementType.Air;
                Debug.Log("I am an Air element now");
               
                break;

            case ElementMain.ElementType.Earth:
                elementMain.currentType = ElementMain.ElementType.Earth;
                Debug.Log("I am an Earth element now");
                //SOUND: (eart element)
                break;
        }
    }

    public void Shoot()
    {
        Debug.Log(bulletDamage, this);
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

        Transform bullet;
        RaycastHit hit;
        if (Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hit, float.MaxValue, layerMask))
        {
            aimPoint = hit.point;
        }
        Vector3 aimDir = (aimPoint - spawnBulletPos.position).normalized;

        if (weaponType == WeaponTypes.Shotgun)
        {
            for (int i = 0; i < 6; i++)
            {
                bullet = Instantiate(bulletPrefab, spawnBulletPos.position, Quaternion.LookRotation(aimDir, Vector3.up));
                bullet.GetComponent<BulletProjectile>().SetVelocity((bullet.forward + new Vector3(Random.Range(-maxBulletSpread, maxBulletSpread), Random.Range(-maxBulletSpread, maxBulletSpread), Random.Range(-maxBulletSpread, maxBulletSpread))) * bulletSpeed);
                bullet.GetComponent<BulletProjectile>().SetElementType(elementMain.currentType);
                bullet.GetComponent<BulletProjectile>().SetDamage(bulletDamage);
            }
        }
        else
        {
            bullet = Instantiate(bulletPrefab, spawnBulletPos.position, Quaternion.LookRotation(aimDir, Vector3.up));
            bullet.GetComponent<BulletProjectile>().SetVelocity(bullet.forward * bulletSpeed);
            bullet.GetComponent<BulletProjectile>().SetElementType(elementMain.currentType);
            bullet.GetComponent<BulletProjectile>().SetDamage(bulletDamage);
        }
        
    }

   

    public virtual void Reload()
    {
        isReloading = true;
        canFire = false;
        reloadEndTime = Time.time + reloadTime;
    }
}
