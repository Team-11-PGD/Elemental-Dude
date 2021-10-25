using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementalGemPickup : PickupMain
{
    [SerializeField]
    private ElementMain element;

    [SerializeField]
    private string PickupText;

    private WeaponSwitcher userWeapon;

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
        PickupText = $"Press E to use {element.currentType} gem.";
        text.powerupText = PickupText;
        text.StartText(false);
    }

    private void OpenConfirmationMenu()
    {
        allowPickup = false;
        confirmMenuOpen = true;

        userWeapon = user.GetComponent<WeaponSwitcher>();
        PickupText = $"Are you sure you want to equip {element.currentType} to your {userWeapon.curWeapon} ? Press F to equip.";
        text.powerupText = PickupText;

        text.StartText(false);
    }

    private void EquipAndRemove()
    {
        confirmMenuOpen = false;

        //equip {element.type} to {current weapon}

        PickupText = $"You equiped {element.currentType} to your {userWeapon.curWeapon}.";
        text.powerupText = PickupText;

        StartCoroutine(RemovePickupOnTimer());
    }
}
