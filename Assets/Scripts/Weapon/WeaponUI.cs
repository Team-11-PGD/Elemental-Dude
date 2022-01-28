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
    private Image GunElement;

    [SerializeField]
    private TextMeshProUGUI extraDamgage;
    [SerializeField]
    private TextMeshProUGUI extraSpeed;

    public Sprite Fe;
    public Sprite We;
    public Sprite Ae;

    [SerializeField]
    private WeaponSwitcher weaponSwitcher;

    private bool HasRun;
    //ElementMain element;

    ElementMain[] elements = new ElementMain[4];

    [SerializeField]
    GameObject guns;

    private Vector3 RifleLocation;
    private Vector3 ShotgunLocation;
    private Vector3 RPGLocation;

    private void Start()
    {
       // Weapon.onElementSwitch += CheckElement;
        RifleLocation = new Vector3(rifleImage.transform.position.x, rifleImage.transform.position.y, 0);
        ShotgunLocation = new Vector3(shotgunImage.transform.position.x, shotgunImage.transform.position.y, 0);
        RPGLocation = new Vector3(rpgImage.transform.position.x, rpgImage.transform.position.y, 0);
        elements = GetComponentsInChildren<ElementMain>();
        Selector.color = Color.white;
        //element = GetComponent<ElementMain>();
    }
    // Update is called once per frame
    void Update()
    {
        switch (weaponSwitcher.curWeapon)
        {
            case Weapon.WeaponTypes.Rifle:
                SwapWeapon(RifleLocation);
                break;
            case Weapon.WeaponTypes.Shotgun:
                SwapWeapon(ShotgunLocation);
                break;
            case Weapon.WeaponTypes.RPG:
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
        Selector.transform.position = gunPosition;
    }
    void CheckElement(ElementMain.ElementType elementType)
    {
        if (elementType == ElementMain.ElementType.Water) { Selector.color = ElementColors.WaterColor; GunElement.sprite = We; }
        if (elementType == ElementMain.ElementType.Air) { Selector.color = ElementColors.AirColor; GunElement.sprite = Ae; }
        if (elementType == ElementMain.ElementType.Fire) { Selector.color = ElementColors.FireColor; GunElement.sprite = Fe; }
    }
}
