using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    [SerializeField] WeaponData weaponData;
    
    public Weapon GetWeapon(WeaponType weaponType)
    {
        return weaponData.GetWeapon(weaponType).Weapon;
    }
    
}
