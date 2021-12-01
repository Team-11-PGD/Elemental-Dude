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
            if (elementRifle.currentType == ElementMain.ElementType.Water) SelectorRifle.color = Color.blue;
            if (elementRifle.currentType == ElementMain.ElementType.Air) SelectorRifle.color = Color.gray;
            if (elementRifle.currentType == ElementMain.ElementType.Fire) SelectorRifle.color = Color.red;
            if (elementRifle.currentType == ElementMain.ElementType.Earth) SelectorRifle.color = new Color(0.59f, 0.38f, 0.21f);
        }
        if (weaponSwitcher.curWeapon == Weapon.WeaponTypes.Shotgun)
        {
            if (elementShotgun.currentType == ElementMain.ElementType.Water) SelectorShotgun.color = Color.blue;
            if (elementShotgun.currentType == ElementMain.ElementType.Air) SelectorShotgun.color = Color.gray;
            if (elementShotgun.currentType == ElementMain.ElementType.Fire) SelectorShotgun.color = Color.red;
            if (elementShotgun.currentType == ElementMain.ElementType.Earth) SelectorShotgun.color = new Color(0.59f, 0.38f, 0.21f);
        }
        if (weaponSwitcher.curWeapon == Weapon.WeaponTypes.RPG)
        {
            if (elementRPG.currentType == ElementMain.ElementType.Water) SelectorRPG.color = Color.blue;
            if (elementRPG.currentType == ElementMain.ElementType.Air) SelectorRPG.color = Color.gray;
            if (elementRPG.currentType == ElementMain.ElementType.Fire) SelectorRPG.color = Color.red;
            if (elementRPG.currentType == ElementMain.ElementType.Earth) SelectorRPG.color = new Color(0.59f, 0.38f, 0.21f);
        }
    }
}
