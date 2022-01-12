using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePickup : PickupMain
{
    [SerializeField] string damagePowerupText = "You picked up extra damage.";

    Weapon[] weapons;

    protected override void PickedUpPickup(Collider player)
    {
        weapons = player.transform.parent.GetComponent<WeaponSwitcher>().weapons;

        foreach (var weapon in weapons)
        {
            weapon.weaponDamage++;
        }

        text.powerupText = damagePowerupText;

        StartCoroutine(RemovePickupOnTimer());
    }
}
