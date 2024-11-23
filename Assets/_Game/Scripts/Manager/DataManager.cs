using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    [SerializeField] WeaponData weaponData;
    [SerializeField] HatsData hatsData;
    [SerializeField] PantData pantData;
    public Weapon GetWeapon(WeaponType weaponType)
    {
        return weaponData.GetWeapon(weaponType).Weapon;
    }

    public Hats GetHat(HatsType hatType)
    {
        return hatsData.GetHat(hatType).Hats;
    }

    public Material GetPant(PantsType pantType)
    {
        return pantData.GetPant(pantType).Material;
    }
  
   


}
