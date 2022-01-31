using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour
{

    [SerializeField]
    private Image Selector;

    [SerializeField]
    private Image rifleImage, shotgunImage, rpgImage;//background images for the weapons

    [SerializeField]
    private TextMeshProUGUI extraSpeed,extraDamage;

    public Sprite fireSprite, waterSprite, airSprite;//sprites for the effective element

    [SerializeField]
    private WeaponSwitcher weaponSwitcher;

    private Vector3 RifleLocation, ShotgunLocation, RPGLocation;

    [SerializeField]
    GunColorHandler gunColorRifle, gunColorShotgun, gunColorRpg;

    private void Start()//Sets the locations for the selector of the weapon images 
    {
        RifleLocation = rifleImage.rectTransform.anchoredPosition = new Vector3(-300,80,0);
        ShotgunLocation = shotgunImage.rectTransform.anchoredPosition = new Vector3(-190, 80, 0);
        RPGLocation = rpgImage.rectTransform.anchoredPosition = new Vector3(-80, 80, 0);
    }
    void Update()
    {
        switch (weaponSwitcher.curWeapon)//switch case for when different weapons are active, sets the locations and calls the color
        {
            case Weapon.WeaponTypes.Rifle://rifle is active
                gunColorRifle.SetColor();
                SwapWeapon(RifleLocation);
                break;

            case Weapon.WeaponTypes.Shotgun://shotgun is active
                gunColorShotgun.SetColor();
                SwapWeapon(ShotgunLocation);
                break;

            case Weapon.WeaponTypes.RPG://rpg is active
                gunColorRpg.SetColor();
                SwapWeapon(RPGLocation);
                break;
            default:
                break;
        }

        extraDamage.text = $"{(weaponSwitcher.ExtraDamage > 0 ? "+" : "")}{weaponSwitcher.ExtraDamage.ToString("0")}";
        extraSpeed.text = $"{(weaponSwitcher.ExtraSpeed > 0 ? "+" : "")}{(weaponSwitcher.ExtraSpeed.ToString())}";
    }
    void SwapWeapon(Vector3 gunPosition)//swaps the location of the weapon selector
    {
        Selector.rectTransform.anchoredPosition = gunPosition;
    }
}
