using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    public Weapon[] weapons;
    public Weapon.WeaponTypes startWeapon;

    private Weapon.WeaponTypes curWeapon;

    void Start()
    {
        SwitchWeapon(startWeapon);
    }

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			SwitchWeapon(Weapon.WeaponTypes.Rifle);
		}
		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			SwitchWeapon(Weapon.WeaponTypes.Shotgun);
		}
		if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			SwitchWeapon(Weapon.WeaponTypes.RPG);
		}
	}

	void SwitchWeapon(Weapon.WeaponTypes weaponType)
	{
		foreach (var weapon in weapons)
		{
            weapon.gameObject.SetActive(weapon.weaponType == weaponType);
		}
        curWeapon = weaponType;
	}
}
