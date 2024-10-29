using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    
    Axe0 = 0,
    Axe1 = 1,
    Boomerang= 3,
    Candy0= 4,
    Candy1= 5,
    Candy2= 6,
    Candy3= 7,
    Candy4= 8,
    Hammer= 9,
    Knife= 10,
    Uzi= 11,
    None = 100
}


[CreateAssetMenu(fileName = "WeaponItemData",menuName = "ScriptableObject/WeaponItemData",order =1)]
public class WeaponItemData:ScriptableObject
{
    [SerializeField] Weapon weapon;
    [SerializeField] WeaponType weaponType;
    public float Price { get; }

    public Weapon Weapon {  get { return weapon; } }
   
}
