using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShop : AShop
{
    public override void ChangeItem()
    {
        WeaponType type = (WeaponType)currentItem;
        character.ChangeWeapon(type);
    }
    public override void BuyItem()
    {
        throw new System.NotImplementedException();
    }
    public override void TakeItem()
    {
        throw new System.NotImplementedException();
    }
}
