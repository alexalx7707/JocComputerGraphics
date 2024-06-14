using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponManager : MonoBehaviour
{
    public Weapon currentWeapon;
    public Weapon[] availableWeapons;

    void Start()
    {
        foreach (Weapon weapon in availableWeapons)
        {
            weapon.gameObject.SetActive(false);
        }

        EquipWeapon(availableWeapons[0]); // Equip the first weapon by default
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            EquipWeapon(availableWeapons[0]);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            EquipWeapon(availableWeapons[1]);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            EquipWeapon(availableWeapons[2]);
        }
    }

    void EquipWeapon(Weapon newWeapon)
    {
        if (currentWeapon != null)
        {
            currentWeapon.gameObject.SetActive(false);
        }

        currentWeapon = newWeapon;
        currentWeapon.gameObject.SetActive(true);
        currentWeapon.UpdateBulletsUI();
    }
}
