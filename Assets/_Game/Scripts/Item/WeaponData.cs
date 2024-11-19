using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponData",menuName = "ScriptableObject/WeaponData",order = 2)]
public class WeaponData : ScriptableObject
{
    [SerializeField] List<WeaponItemData> wpItemDatas;
    
    public int CountWp => wpItemDatas.Count;
    public WeaponItemData GetWeapon(WeaponType weaponType)
    {
        return wpItemDatas[(int)weaponType];
    }

}

