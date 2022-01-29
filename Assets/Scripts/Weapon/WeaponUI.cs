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
    private Image rifleImage, shotgunImage, rpgImage;

    [SerializeField]
    private TextMeshProUGUI extraDamgage;
    [SerializeField]
    private TextMeshProUGUI extraSpeed;

    public Sprite fireSprite, waterSprite, airSprite;

    [SerializeField]
    private WeaponSwitcher weaponSwitcher;

    private Vector3 RifleLocation;
    private Vector3 ShotgunLocation;
    private Vector3 RPGLocation;

    [SerializeField]
    GunColorHandler gunColorRifle, gunColorShotgun, gunColorRpg;

    private void Start()
    {
        RifleLocation = rifleImage.rectTransform.anchoredPosition = new Vector3(-300, 80, 0);
        ShotgunLocation = shotgunImage.rectTransform.anchoredPosition = new Vector3(-190, 80, 0);
        RPGLocation = rpgImage.rectTransform.anchoredPosition = new Vector3(-80, 80, 0);
        Selector.color = Color.white;
    }
    // Update is called once per frame
    void Update()
    {
        switch (weaponSwitcher.curWeapon)
        {
            case Weapon.WeaponTypes.Rifle:
                gunColorRifle.SetColor();
                SwapWeapon(RifleLocation);
                break;
            case Weapon.WeaponTypes.Shotgun:
                gunColorShotgun.SetColor();
                SwapWeapon(ShotgunLocation);
                break;
            case Weapon.WeaponTypes.RPG:
                gunColorRpg.SetColor();
                SwapWeapon(RPGLocation);
                break;
            default:
                break;
        }

        extraDamgage.text = $"{(weaponSwitcher.ExtraDamage > 0 ? "+" : "")}{weaponSwitcher.ExtraDamage.ToString("0")}";
        extraSpeed.text = $"{(weaponSwitcher.ExtraSpeed > 0 ? "+" : "")}{(weaponSwitcher.ExtraSpeed.ToString())}";
    }
    void SwapWeapon(Vector3 gunPosition)
    {
        Selector.rectTransform.anchoredPosition = gunPosition;
    }
}
