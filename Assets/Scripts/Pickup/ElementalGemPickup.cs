using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElementalGemPickup : PickupMain
{
    [SerializeField]
    private ElementMain element;
    private WeaponSwitcher userWeapon;

    [SerializeField]
    private Image fireElement;
    [SerializeField]
    private Image waterElement;
    [SerializeField]
    private Image airElement;

    private string PickupText;

    private bool inPickup = false;
    private bool confirmMenuOpen = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && inPickup) OpenConfirmationMenu();
        if (Input.GetKeyDown(KeyCode.F) && confirmMenuOpen) EquipAndRemove();
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.tag == "Player") && allowPickup)
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
        if (element.currentType == ElementMain.ElementType.Fire) fireElement.enabled = true; else fireElement.enabled = false;
        if (element.currentType == ElementMain.ElementType.Water) waterElement.enabled = true; else waterElement.enabled = false;
        if (element.currentType == ElementMain.ElementType.Air) airElement.enabled = true; else airElement.enabled = false;
        PickupText = $"Press E to use {element.currentType} gem.";
        text.powerupText = PickupText;
        text.StartText(false);
    }

    private void OpenConfirmationMenu()
    {
        allowPickup = false;
        confirmMenuOpen = true;

        userWeapon = user.GetComponentInParent<WeaponSwitcher>();
        PickupText = $"Are you sure you want to equip {element.currentType} to your {userWeapon.curWeapon} ? Press F to equip.";
        text.powerupText = PickupText;

        text.StartText(false);
    }

    private void EquipAndRemove()
    {
        confirmMenuOpen = false;

        if (userWeapon.GetWeapon(userWeapon.curWeapon).elementMain.currentType == ElementMain.ElementType.None) //do switch
        {
            userWeapon.GetWeapon(userWeapon.curWeapon).SetWeaponElement(element.currentType);
            PickupText = $"You equiped {element.currentType} to your {userWeapon.curWeapon}.";
            text.powerupText = PickupText;

            StartCoroutine(RemovePickupOnTimer());
        }
        else //dont switch
        {
            PickupText = $"You already have {userWeapon.GetWeapon(userWeapon.curWeapon).elementMain.currentType} on your {userWeapon.curWeapon}.";
            text.powerupText = PickupText;
            text.StartText();
        }
    }
}
