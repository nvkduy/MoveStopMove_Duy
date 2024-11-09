using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    
    Axe0 = 0,
    Axe1 = 1,
    Boomerang= 2,
    Candy0= 3,
    Candy1= 4,
    Candy2= 5,
    Candy3= 6,
    Candy4= 7,
    Hammer= 8,
    Knife= 9,
    Uzi= 10,
    None = 100
}

[CreateAssetMenu(fileName = "WeaponItemData",menuName = "ScriptableObject/WeaponItemData",order =1)]
public class WeaponItemData:ScriptableObject
{
    [SerializeField] Weapon weapon;
    [SerializeField] WeaponType weaponType;

    public string name;
    public int price;
    public bool isUnlocked;
    public Weapon Weapon {  get { return weapon; } }
   
}
