using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePickup : PickupMain
{
    // private Health userHealth;
     private Projectile userDamage;
    int newUserDamage = 20;

    //Projectile

     [SerializeField]
    private string damagePowerupText = "You picked up extra damage.";

    protected override void PickedUpPickup()
    {
      
        //userDamage = new Projectile();
        
       // userDamage.damageAmount = newUserDamage;
        //Debug.Log("after " + userDamage.damageAmount);
        userDamage = user.GetComponentInParent<Projectile>();
        userDamage.damageAmount = 20;
        //userDamage = userDamage.damageAmount;
        text.powerupText = damagePowerupText;
       // userHealth.Heal(20);

        StartCoroutine(RemovePickupOnTimer());
    }
}
