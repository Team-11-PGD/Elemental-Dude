using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : PickupMain
{
    private Health userHealth;

    [SerializeField]
    private string healthPowerupText = "You picked up health.";

    protected override void PickedUpPowerup()
    {
        userHealth = user.GetComponent<Health>();
        text.powerupText = healthPowerupText;
        userHealth.Heal(20);
        base.PickedUpPowerup();
    }
}
