using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Chris Huider
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

    /// <summary>
    /// Show gem type text on UI.
    /// </summary>
    private void ShowGemText()
    {
        if (element.currentType == ElementMain.ElementType.Fire) fireElement.enabled = true; else fireElement.enabled = false;      //<-
        if (element.currentType == ElementMain.ElementType.Water) waterElement.enabled = true; else waterElement.enabled = false;   //Check elemental type.
        if (element.currentType == ElementMain.ElementType.Air) airElement.enabled = true; else airElement.enabled = false;         //<-

        PickupText = $"Press E to use {element.currentType} gem.";                                                                  //<-
        text.powerupText = PickupText;                                                                                              //Changing and showing text.
        text.StartText(false);                                                                                                      //<-
    }

    /// <summary>
    /// Open confirmation menu on UI.
    /// </summary>
    private void OpenConfirmationMenu()
    {
        allowPickup = false;
        confirmMenuOpen = true;

        userWeapon = user.GetComponentInParent<WeaponSwitcher>();

        PickupText = $"Are you sure you want to equip {element.currentType} to your {userWeapon.curWeapon} ? Press F to equip.";    //<-
        text.powerupText = PickupText;                                                                                              //Changing and showing text.
        text.StartText(false);                                                                                                      //<-
    }

    /// <summary>
    /// Equip element to current weapon and remove pickup GameObject.
    /// </summary>
    private void EquipAndRemove()
    {
        confirmMenuOpen = false;

        if (userWeapon.GetWeapon(userWeapon.curWeapon).elementMain.currentType == ElementMain.ElementType.None) //do switch
        {
            userWeapon.GetWeapon(userWeapon.curWeapon).SetWeaponElement(element.currentType);                                       //Setting new element to current weapon.

            PickupText = $"You equiped {element.currentType} to your {userWeapon.curWeapon}.";                                      //Changing and showing text.
            text.powerupText = PickupText;                                                                                          //<-

            StartCoroutine(RemovePickupOnTimer());
        }
        else //dont switch
        {
            PickupText = $"You already have {userWeapon.GetWeapon(userWeapon.curWeapon).elementMain.currentType} on your {userWeapon.curWeapon}.";      //<-
            text.powerupText = PickupText;                                                                                                              //Changing and showing text.
            text.StartText();                                                                                                                           //<-
        }
    }
}
