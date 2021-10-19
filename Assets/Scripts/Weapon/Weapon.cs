using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponTypes weaponType;
    public EquipedElement equipedElement;

    public Transform spawnBulletPos;
    public Transform bulletPrefab;
    public Camera playerCam;

    public int bulletAmount;
    public int maxBullets;
    public float reloadTime;
    public float fireInterval;
    public float maxBulletSpread = 0.02f;
    public float bulletSpeed = 40;

    private bool canFire = true;
    private bool isReloading;
    private float timeToFire;
    private float reloadEndTime;
    private Vector3 aimPoint;


	public enum WeaponTypes
    { 
        Rifle,
        Shotgun,
        RPG
    }

    public enum EquipedElement
	{
        None,
        Water,
        Fire,
        Air,
        Earth
    }

    void Start()
    {
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

        if(isReloading && Time.time >= reloadEndTime)
		{
            canFire = true;
            isReloading = false;
            bulletAmount = maxBullets;

            Debug.Log("Done reloading!");
		}
    }

    public void ElementalWeapon(EquipedElement equipedElement)
	{
        switch (equipedElement)
        {
            case EquipedElement.None:
                
                break;

            case EquipedElement.Water:
                
                break;

            case EquipedElement.Fire:
                
                break;

            case EquipedElement.Air:
               
                break;

            case EquipedElement.Earth:
                
                break;
        }
    }

    public void Shoot()
    {
        if (bulletAmount <= 0)
        {
            Debug.Log("PRESS R TO RELOAD");
            canFire = false;
            return;
        }
        else
        {
            bulletAmount -= 1;
 
            if(bulletAmount <= 0)
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
