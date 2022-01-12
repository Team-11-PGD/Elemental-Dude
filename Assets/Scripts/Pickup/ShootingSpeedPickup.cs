using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Chris Huider
public class ShootingSpeedPickup : PickupMain
{

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
        //SOUND: (pick up sound)
        text.powerupText = shootingSpeedPowerupText;
        IncreaseShootingSpeed();

        StartCoroutine(RemovePickupOnTimer());
    }
}
