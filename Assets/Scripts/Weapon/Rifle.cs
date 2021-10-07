using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : Weapon
{
    void Start()
    {
        bulletAmount = 5;
        maxBullets = 30;
        reloadTime = 3;
        timeToFire = 0;
        fireInterval = 0.4f;
    }

    void Update()
    {
        //Debug.Log(bulletAmount);
        if (Input.GetButton("Fire1") && Time.time >= timeToFire && canFire)
        {
            timeToFire = Time.time + fireInterval;
            Shoot();
        }


    }
}
