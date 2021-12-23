using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingSpeedPickup : PickupMain
{

    [SerializeField]
    private string shootingSpeedPowerupText = "You picked up extra shooting speed.";

    private WeaponSwitcher userWeapon;

    private void IncreaseShootingSpeed()
    {
        userWeapon = user.GetComponentInParent<WeaponSwitcher>();
        userWeapon.GetWeapon(userWeapon.curWeapon).fireInterval -= 0.02f;
    }

    protected override void PickedUpPickup()
    {
        //SOUND: (pick up sound)
        text.powerupText = shootingSpeedPowerupText;
        IncreaseShootingSpeed();

        StartCoroutine(RemovePickupOnTimer());
    }
}
