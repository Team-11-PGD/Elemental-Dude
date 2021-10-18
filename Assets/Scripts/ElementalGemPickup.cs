using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementalGemPickup : PickupMain
{
    [SerializeField]
    private string ElementalGemPickupText = "You picked up health.";

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            //equip element to current weapon.
        }
    }

    protected override void PickedUpPowerup()
    {
        //this is not how i want it.
        text.powerupText = ElementalGemPickupText;
        
        //text should be shown saying do you want to equip {this element} to {this weapon}?

        base.PickedUpPowerup();
    }
}
