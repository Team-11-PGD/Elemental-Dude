using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : PickupMain
{
    ///example code
    //public Player player;
    [SerializeField]
    private string healthPowerupText = "You picked up health.";

    private void AddHealth()
    {
        ///example code
        //player.healthAmount += 50;
    }

    protected override void PickedUpPowerup()
    {
        text.powerupText = healthPowerupText;
        AddHealth();

        base.PickedUpPowerup();
    }
}
