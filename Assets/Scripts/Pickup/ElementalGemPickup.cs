using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementalGemPickup : PickupMain
{
    [SerializeField]
    private ElementMain element;

    [SerializeField]
    private string PickupText;

    private Weapon userWeapon;

    private bool inPickup = false;
    private bool confirmMenuOpen = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && inPickup) OpenConfirmationMenu();
        if (Input.GetKeyDown(KeyCode.F) && confirmMenuOpen) EquipAndRemove();
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if ((other.name == "Player") && allowPickup)
        {
            user = other;
            userWeapon = user.GetComponent<Weapon>();
            inPickup = true;
            ShowGemText();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        inPickup = false;
        allowPickup = true;
        confirmMenuOpen = false;
        text.uiObject.SetActive(false);
    }

    private void ShowGemText()
    {
        PickupText = $"Press E to use {element.type} gem.";
        text.powerupText = PickupText;
        text.StartText(false);
    }

    private void OpenConfirmationMenu()
    {
        allowPickup = false;
        confirmMenuOpen = true;

        PickupText = $"Are you sure you want to equip {element.type} to your current weapon? Press F to equip.";
        //Switch "current weapon" to the name of the current weapon. Can't do it now because i have the old weapon scripts.
        text.powerupText = PickupText;

        text.StartText(false);
    }

    private void EquipAndRemove()
    {
        confirmMenuOpen = false;

        //equip {element.type} to {current weapon}

        PickupText = $"You equiped {element.type} to your current weapon.";
        //Switch "current weapon" to the name of the current weapon. Can't do it now because i have the old weapon scripts.
        text.powerupText = PickupText;

        StartCoroutine(RemovePickupOnTimer());
    }
}
