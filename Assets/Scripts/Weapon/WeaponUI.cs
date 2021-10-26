using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Image Rifle;
    [SerializeField]
    private Image RPG;
    [SerializeField]
    private Image Shotgun;
    [SerializeField]
    private WeaponSwitcher weaponSwitcher;

        // Update is called once per frame
        void Update()
        {
        if (weaponSwitcher.curWeapon == Weapon.WeaponTypes.Rifle)
        {
            Rifle.enabled = true;
        }
        else Rifle.enabled = false;

            if (weaponSwitcher.curWeapon == Weapon.WeaponTypes.RPG)
            {
                RPG.enabled = true;
            }
        else RPG.enabled = false;

        if (weaponSwitcher.curWeapon == Weapon.WeaponTypes.Shotgun)
            {
                Shotgun.enabled = true;
            }
        else Shotgun.enabled = false;
    }
    
}
