using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponTypes weaponType;
    public Camera playerCam;

    [Header("Bullet")]
    public Transform spawnBulletPos;
    public Transform bulletPrefab;
    public int curBulletAmount;
    public int maxBullets;
    public float maxBulletSpread = 0.02f;
    public float bulletSpeed = 40;

    [Header("Timers")]
    public float reloadTime;
    public float fireInterval;
   
    private bool canFire = true;
    private bool isReloading;
    private float timeToFire;
    private float reloadEndTime;
    private Vector3 aimPoint;

    private ElementMain elementMain;

	public enum WeaponTypes
    { 
        Rifle,
        Shotgun,
        RPG
    }

    void Start()
    {
        elementMain = GetComponent<ElementMain>();
    }

    void Update()
    {
        Debug.DrawRay(playerCam.transform.position, playerCam.transform.forward * 50, Color.green);

        if (Input.GetButton("Fire1") && Time.time >= timeToFire && canFire)
        {
            timeToFire = Time.time + fireInterval;
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.R) && !canFire && !isReloading)
        {
            Reload();
        }

        if (isReloading && Time.time >= reloadEndTime)
		{
            canFire = true;
            isReloading = false;
            curBulletAmount = maxBullets;

            Debug.Log("Done reloading!");
		}
    }

    public void ElementWeaponChange()
	{
        switch (elementMain.currentType)
        {
            case ElementMain.ElementType.None:
                Debug.Log("I am a None element now");
                break;

            case ElementMain.ElementType.Water:
                Debug.Log("I am a Water element now");
                break;

            case ElementMain.ElementType.Fire:
                Debug.Log("I am a Fire element now");
                break;

            case ElementMain.ElementType.Air:
                Debug.Log("I am an Air element now");
                break;

            case ElementMain.ElementType.Earth:
                Debug.Log("I am an Earth element now");
                break;
        }
    }

    public void Shoot()
    {
        if (curBulletAmount <= 0)
        {
            Debug.Log("PRESS R TO RELOAD");
            canFire = false;
            return;
        }
        else
        {
            curBulletAmount -= 1;
 
            if(curBulletAmount <= 0)
			{
                canFire = false;
                Debug.Log("PRESS R TO RELOAD");
            }
        }

        RaycastHit hit;
        if (Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hit, 50f))
        {
            //Debug.Log(hit.transform.name);
            aimPoint = hit.point;
        }
        Vector3 aimDir = (aimPoint - spawnBulletPos.position).normalized; 

        if (weaponType == WeaponTypes.Shotgun)
        {
            for (int i = 0; i < 6; i++)
			{
				Transform bullet = Instantiate(bulletPrefab, spawnBulletPos.position, Quaternion.LookRotation(aimDir, Vector3.up));
                bullet.GetComponent<BulletProjectice>().SetVelocity((bullet.forward + new Vector3(Random.Range(-maxBulletSpread, maxBulletSpread),Random.Range(-maxBulletSpread, maxBulletSpread),Random.Range(-maxBulletSpread, maxBulletSpread))) * bulletSpeed);
            }
        }
        else
        {
            Transform bullet = Instantiate(bulletPrefab, spawnBulletPos.position, Quaternion.LookRotation(aimDir, Vector3.up));
            bullet.GetComponent<BulletProjectice>().SetVelocity(bullet.forward * bulletSpeed);
        }
    }

    public virtual void Reload()
	{
        isReloading = true;
        reloadEndTime = Time.time + reloadTime;
	}
}
