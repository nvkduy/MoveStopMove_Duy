using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    [SerializeField] WeaponData weaponData;
    [SerializeField] HatsData hatsData;
    public Weapon GetWeapon(WeaponType weaponType)
    {
        return weaponData.GetWeapon(weaponType).Weapon;
    }

    public Character GetHats(HatType hatsType)
    {
        return hatsData.GetHat(hatsType).Character;
    }


    
}
