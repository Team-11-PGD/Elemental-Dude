using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Image SelectorRifle;
    [SerializeField]
    private Image SelectorShotgun;
    [SerializeField]
    private Image SelectorRPG;

    [SerializeField]
    private WeaponSwitcher weaponSwitcher;

    [SerializeField]
    private ElementMain elementRifle;
    [SerializeField]
    private ElementMain elementShotgun;
    [SerializeField]
    private ElementMain elementRPG;

    // Update is called once per frame
    void Update()
    {
        if (weaponSwitcher.curWeapon == Weapon.WeaponTypes.Rifle)
        {
            SelectorRifle.enabled = true;
        }
        else SelectorRifle.enabled = false;

        if (weaponSwitcher.curWeapon == Weapon.WeaponTypes.RPG)
        {
            SelectorRPG.enabled = true;
        }
        else SelectorRPG.enabled = false;

        if (weaponSwitcher.curWeapon == Weapon.WeaponTypes.Shotgun)
        {
            SelectorShotgun.enabled = true;
        }
        else SelectorShotgun.enabled = false;

        //color changer for the Ui.
        if (weaponSwitcher.curWeapon == Weapon.WeaponTypes.Rifle)
        {
            if (elementRifle.currentType == ElementMain.ElementType.Water) SelectorRifle.color = ElementMain.ElementWater;
            if (elementRifle.currentType == ElementMain.ElementType.Air) SelectorRifle.color = ElementMain.ElementAir;
            if (elementRifle.currentType == ElementMain.ElementType.Fire) SelectorRifle.color = ElementMain.ElementFire;
            if (elementRifle.currentType == ElementMain.ElementType.Earth) SelectorRifle.color = ElementMain.ElementEarth;
        }
        if (weaponSwitcher.curWeapon == Weapon.WeaponTypes.Shotgun)
        {
            if (elementShotgun.currentType == ElementMain.ElementType.Water) SelectorShotgun.color = ElementMain.ElementWater;
            if (elementShotgun.currentType == ElementMain.ElementType.Air) SelectorShotgun.color = ElementMain.ElementAir;
            if (elementShotgun.currentType == ElementMain.ElementType.Fire) SelectorShotgun.color = ElementMain.ElementFire;
            if (elementShotgun.currentType == ElementMain.ElementType.Earth) SelectorShotgun.color = ElementMain.ElementEarth;
        }
        if (weaponSwitcher.curWeapon == Weapon.WeaponTypes.RPG)
        {
            if (elementRPG.currentType == ElementMain.ElementType.Water) SelectorRPG.color = ElementMain.ElementWater;
            if (elementRPG.currentType == ElementMain.ElementType.Air) SelectorRPG.color = ElementMain.ElementAir;
            if (elementRPG.currentType == ElementMain.ElementType.Fire) SelectorRPG.color = ElementMain.ElementFire;
            if (elementRPG.currentType == ElementMain.ElementType.Earth) SelectorRPG.color = ElementMain.ElementEarth;
        }
    }
}
