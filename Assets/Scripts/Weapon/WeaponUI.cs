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
    private Image rifleEffective;
    [SerializeField]
    private Image shotgunEffective;
    [SerializeField]
    private Image rgpEffective;

    [SerializeField]
    private Text RifleMod;
    [SerializeField]
    private Text ShotgunMod;
    [SerializeField]
    private Text RpgMod;

    public Sprite Fe;
    public Sprite We;
    public Sprite Ae;

    [SerializeField]
    private WeaponSwitcher weaponSwitcher;

    [SerializeField]
    private ElementMain elementRifle;
    [SerializeField]
    private ElementMain elementShotgun;
    [SerializeField]
    private ElementMain elementRPG;

    private bool allowShowRifle = false;
    private bool allowShowShotgun = false;
    private bool allowShowRpg = false;

    // Update is called once per frame
    void Update()
    {
        if (weaponSwitcher.curWeapon == Weapon.WeaponTypes.Rifle)
        {
            SelectorRifle.enabled = true;
            if (allowShowRifle) { rifleEffective.enabled = true; RifleMod.enabled = true; }
        }
        else { SelectorRifle.enabled = false; rifleEffective.enabled = false; RifleMod.enabled = false; }

        if (weaponSwitcher.curWeapon == Weapon.WeaponTypes.RPG)
        {
            SelectorRPG.enabled = true;
            if (allowShowRpg) { rgpEffective.enabled = true; RpgMod.enabled = true; }
        }
        else { SelectorRPG.enabled = false; rgpEffective.enabled = false; RpgMod.enabled = false; }

        if (weaponSwitcher.curWeapon == Weapon.WeaponTypes.Shotgun)
        {
            SelectorShotgun.enabled = true;
            if (allowShowShotgun) { shotgunEffective.enabled = true; ShotgunMod.enabled = true; }
            }
        else { SelectorShotgun.enabled = false; shotgunEffective.enabled = false; ShotgunMod.enabled = false; }

        //color changer for the Ui.
        if (weaponSwitcher.curWeapon == Weapon.WeaponTypes.Rifle)
        {
            if (elementRifle.currentType == ElementMain.ElementType.Water) { SelectorRifle.color = ElementColors.WaterColor; rifleEffective.sprite = Fe; allowShowRifle = true; }
            if (elementRifle.currentType == ElementMain.ElementType.Air) { SelectorRifle.color = ElementColors.AirColor; rifleEffective.sprite = We; allowShowRifle = true; }
            if (elementRifle.currentType == ElementMain.ElementType.Fire) { SelectorRifle.color = ElementColors.FireColor; rifleEffective.sprite = Ae; allowShowRifle = true; }
        }
        if (weaponSwitcher.curWeapon == Weapon.WeaponTypes.Shotgun)
        {
            if (elementShotgun.currentType == ElementMain.ElementType.Water) { SelectorShotgun.color = ElementColors.WaterColor; shotgunEffective.sprite = Fe; allowShowShotgun = true; }
            if (elementShotgun.currentType == ElementMain.ElementType.Air){ SelectorShotgun.color = ElementColors.AirColor; shotgunEffective.sprite = We; allowShowShotgun = true; }
            if (elementShotgun.currentType == ElementMain.ElementType.Fire){ SelectorShotgun.color = ElementColors.FireColor; shotgunEffective.sprite = Ae; allowShowShotgun = true; }
        }
        if (weaponSwitcher.curWeapon == Weapon.WeaponTypes.RPG)
        {
            if (elementRPG.currentType == ElementMain.ElementType.Water) { SelectorRPG.color = ElementColors.WaterColor; rgpEffective.sprite = Fe; allowShowRpg = true; }
            if (elementRPG.currentType == ElementMain.ElementType.Air){ SelectorRPG.color = ElementColors.AirColor; rgpEffective.sprite = We; allowShowRpg = true; }
            if (elementRPG.currentType == ElementMain.ElementType.Fire){ SelectorRPG.color = ElementColors.FireColor; rgpEffective.sprite = Ae; allowShowRpg = true; }
        }
    }
}
