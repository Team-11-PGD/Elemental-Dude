using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    public Weapon[] weapons;
    public Weapon.WeaponTypes startWeapon;

    public Weapon.WeaponTypes curWeapon;

    float _extraDamage = 0, _extraSpeed = 0;
    public float ExtraDamage
    {
        get
        {
            return _extraDamage;
        }
        set
        {
            foreach (Weapon weapon in weapons)
            {
                weapon.extraDamage = value;
            }
            _extraDamage = value;
        }
    }
    public float ExtraSpeed
    {
        get
        {
            return _extraSpeed;
        }
        set
        {
            foreach (Weapon weapon in weapons)
            {
                weapon.extraSpeed = value;
            }
            _extraSpeed = value;
        }
    }

    void Start()
    {
		GetWeapon(Weapon.WeaponTypes.Rifle).gameObject.SetActive(false);
		GetWeapon(Weapon.WeaponTypes.Shotgun).gameObject.SetActive(false);
		GetWeapon(Weapon.WeaponTypes.RPG).gameObject.SetActive(false);

		SwitchWeapon(startWeapon);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //SOUND: (switch to rifle)
            Debug.Log("switch to rifle");
            SwitchWeapon(Weapon.WeaponTypes.Rifle);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            //SOUND: (switch to shotgyn)
            Debug.Log("switch to shotty");
            SwitchWeapon(Weapon.WeaponTypes.Shotgun);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            //SOUND: (switch to rpg)
            Debug.Log("switch to rpg");
            SwitchWeapon(Weapon.WeaponTypes.RPG);
        }
    }


    void SwitchWeapon(Weapon.WeaponTypes weaponType)
    {
	    if(curWeapon != weaponType)
			AudioManager.instance.PlaySoundFromWorld(AudioManager.instance.GunSounds, "WeaponSwitch");

        GetWeapon(curWeapon).gameObject.SetActive(false);
        GetWeapon(weaponType).gameObject.SetActive(true);
        curWeapon = weaponType;
        
    }

    public Weapon GetWeapon(Weapon.WeaponTypes weaponType)
    {
        foreach (var weapon in weapons)
        {
            if (weapon.weaponType == weaponType)
            {
                return weapon;
            }
        }
        return null;
    }
}
