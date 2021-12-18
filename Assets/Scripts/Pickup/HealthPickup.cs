using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Chris Huider
public class HealthPickup : PickupMain
{
    private Health userHealth;

    [SerializeField]
    private string healthPowerupText = "You picked up health.";

    protected override void PickedUpPickup()
    {
        userHealth = user.GetComponentInParent<Health>();
        text.powerupText = healthPowerupText;
        userHealth.Heal(20);

        StartCoroutine(RemovePickupOnTimer());
    }
}
