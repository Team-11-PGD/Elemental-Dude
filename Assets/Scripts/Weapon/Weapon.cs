using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponTypes weaponType;

    public Transform spawnBulletPos;
    public Transform bulletPrefab;
    public Camera playerCam;

    public int bulletAmount;
    public int maxBullets;
    public float reloadTime;
    public float timeToFire;
    public float fireInterval;
    
    private bool canFire = true;
    private bool isReloading;
    private float reloadEndTime;
    private Vector3 aimPoint;

	public enum WeaponTypes
    { 
        Rifle,
        Shotgun,
        RPG
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

    public virtual void Shoot()
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

            //this is pure for debug purposes
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
        Instantiate(bulletPrefab, spawnBulletPos.position, Quaternion.LookRotation(aimDir, Vector3.up));
    }

    public virtual void Reload()
	{
        isReloading = true;
        reloadEndTime = Time.time + reloadTime;
	}
}
