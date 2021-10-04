using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int bulletAmount;
    public int reloadTime;
    public Transform spawnBulletPos;
    public Transform bulletPrefab;
    public Camera playerCam;

    private float timeToFire = 0;
    private float fireInterval = 0.4f;
    private Vector3 mouseWorldPos;

    public enum WeaponType
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
        if (Input.GetButton("Fire1") && Time.time >= timeToFire)
        {
            timeToFire = Time.time + fireInterval;
            Shoot();
        }
    }

    private void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hit, 999f))
        {
            Debug.Log(hit.transform.name);
            mouseWorldPos = hit.point;
        }
        Debug.DrawRay(playerCam.transform.position, playerCam.transform.forward * 50, Color.green);

        Vector3 aimDir = (mouseWorldPos - spawnBulletPos.position).normalized;
        Instantiate(bulletPrefab, spawnBulletPos.position, Quaternion.LookRotation(aimDir, Vector3.up));
    }
}
