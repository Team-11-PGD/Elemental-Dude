using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Chris Huider
public class ShootingSpeedPickup : PickupMain
{
    [SerializeField] float amount = 0.1f;

    [SerializeField]
    private string shootingSpeedPowerupText = "You picked up extra shooting speed.";
    ///example code
    //public Player player;

    private void IncreaseShootingSpeed()
    {
        ///example code
        //player.shootingSpeed += 0.1f;
    }

    protected override void PickedUpPickup(Collider player)
    {
        WeaponSwitcher weapon = player.transform.parent.GetComponent<WeaponSwitcher>();
        weapon.ExtraSpeed += amount;

        text.powerupText = shootingSpeedPowerupText;
        IncreaseShootingSpeed();

        AudioManager.instance.PlaySoundFromObject(AudioManager.instance.SoundEffects, this.gameObject, "ShootSpeedPickup");

        StartCoroutine(RemovePickupOnTimer());
    }
}
