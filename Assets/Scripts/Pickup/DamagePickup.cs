using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePickup : PickupMain
{
    
     private Weapon weapon;
     float oldDamage;
    

   

     [SerializeField]
    private string damagePowerupText = "You picked up extra damage.";

    protected override void PickedUpPickup(Collider player)
    {

        weapon = player.transform.parent.GetComponent > { Weapon();


        // userDamage.damageAmount = newUserDamage;
        //Debug.Log("after " + userDamage.damageAmount);

        oldDamage = weapon.bulletDamage;
        weapon.bulletDamage =  20;
        
        Debug.Log(" pickup" + weapon.bulletDamage, weapon);

        //userDamage = userDamage.damageAmount;
        text.powerupText = damagePowerupText;
      

        StartCoroutine(RemovePickupOnTimer());
    }
}
