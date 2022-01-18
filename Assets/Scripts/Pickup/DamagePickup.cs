using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePickup : PickupMain
{
    [SerializeField] float amount = 1f;
    [SerializeField] string damagePowerupText = "You picked up extra damage.";

    protected override void PickedUpPickup(Collider player)
    {
        WeaponSwitcher weapon = player.GetComponent<WeaponSwitcher>();

        weapon.ExtraDamage += amount;

        text.powerupText = damagePowerupText;

        StartCoroutine(RemovePickupOnTimer());
    }
}
