using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class WeaponShop : UIShop
{
    [SerializeField]
    private WeaponData weaponData;

   public override void ChangeItem()
    {
        currentItemData = weaponData.GetWeapon((WeaponType)currentItem);
        UpdateUI();
        PreviewItem(currentItemData);
    }


    protected override void EquipItem(ItemData itemData)
    {
        WeaponItemData weaponItem = itemData as WeaponItemData;
        if (weaponItem != null)
        {
            PlayerPrefs.SetInt("currrenWeapon",(int) weaponItem.WeaponType );
            
        }
    }

    protected override void PreviewItem(ItemData itemData)
    {
        WeaponItemData weaponItem = itemData as WeaponItemData;
        if (weaponItem != null)
        {
            character.PreviewWeapon(weaponItem.WeaponType);
        }
    }

}
