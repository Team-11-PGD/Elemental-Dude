using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    public Weapon[] weapons;
    public Weapon.WeaponTypes startWeapon;

    public Weapon.WeaponTypes curWeapon;

    void Start()
    {
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
		GetWeapon(curWeapon).gameObject.SetActive(false);
		GetWeapon(weaponType).gameObject.SetActive(true);
		curWeapon = weaponType;
	}

	public Weapon GetWeapon(Weapon.WeaponTypes weaponType)
    {
		foreach (var weapon in weapons)
		{
			if(weapon.weaponType == weaponType)
            {
				return weapon;
			}
		}
		return null;
	}
}
