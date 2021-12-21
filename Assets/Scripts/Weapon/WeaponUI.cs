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
            if (elementRifle.currentType == ElementMain.ElementType.Water) SelectorRifle.color = ElementColors.WaterColor;
            if (elementRifle.currentType == ElementMain.ElementType.Air) SelectorRifle.color = ElementColors.AirColor;
            if (elementRifle.currentType == ElementMain.ElementType.Fire) SelectorRifle.color = ElementColors.FireColor;
            if (elementRifle.currentType == ElementMain.ElementType.Earth) SelectorRifle.color = ElementColors.EarthColor;
        }
        if (weaponSwitcher.curWeapon == Weapon.WeaponTypes.Shotgun)
        {
            if (elementShotgun.currentType == ElementMain.ElementType.Water) SelectorShotgun.color = ElementColors.WaterColor;
            if (elementShotgun.currentType == ElementMain.ElementType.Air) SelectorShotgun.color = ElementColors.AirColor;
            if (elementShotgun.currentType == ElementMain.ElementType.Fire) SelectorShotgun.color = ElementColors.FireColor;
            if (elementShotgun.currentType == ElementMain.ElementType.Earth) SelectorShotgun.color = ElementColors.EarthColor;
        }
        if (weaponSwitcher.curWeapon == Weapon.WeaponTypes.RPG)
        {
            if (elementRPG.currentType == ElementMain.ElementType.Water) SelectorRPG.color = ElementColors.WaterColor;
            if (elementRPG.currentType == ElementMain.ElementType.Air) SelectorRPG.color = ElementColors.AirColor;
            if (elementRPG.currentType == ElementMain.ElementType.Fire) SelectorRPG.color = ElementColors.FireColor;
            if (elementRPG.currentType == ElementMain.ElementType.Earth) SelectorRPG.color = ElementColors.EarthColor;
        }
    }
}
