using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform spawnBulletPos;
    public Transform bulletPrefab;
    public Camera playerCam;

    public int bulletAmount;
    public int maxBullets;
    public int reloadTime;
    public float timeToFire;
    public float fireInterval;
    public bool canFire = true;

    private Vector3 aimPoint;

    void Start()
    {
    }

    void Update()
    {
        Debug.DrawRay(playerCam.transform.position, playerCam.transform.forward * 50, Color.green);

        if (Input.GetKeyDown(KeyCode.R) && !canFire)
        {
            Debug.Log("bigballs");
        }
    }

    public virtual void Shoot()
    {
        if (bulletAmount <= 0)
        {
            canFire = false;
        }
        else
        {
            bulletAmount -= 1;
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
        Debug.Log("PRESS R TO RELOAD");
	}
}
