using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Chris Huider
public class HealthPickup : PickupMain
{
    private Health userHealth;

    [SerializeField]
    private string healthPowerupText = "You picked up health.";

    protected override void PickedUpPickup(Collider player)
    {
        userHealth = user.GetComponentInParent<Health>();
        text.powerupText = healthPowerupText;
        userHealth.Heal(20);

        AudioManager.instance.PlaySoundFromObject(AudioManager.instance.SoundEffects, this.gameObject, "HealthPickup");

        StartCoroutine(RemovePickupOnTimer());
    }
}
