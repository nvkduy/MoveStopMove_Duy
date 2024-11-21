using UnityEngine;

public class CanvasWeaponShop : UIShop
{
    [SerializeField] WeaponData weaponData;


    protected override void SetMaxNumberItem()
    {
        maxNumberOfData = weaponData.CountWp;
    }
    protected override void ChangeItem()
    {
        currentItemData = weaponData.GetWeapon((WeaponType)currentItem);
        UpdateUI();
        //PreviewItem(currentItemData);
        UpdatePreview(currentItemData.icon);
    }


    protected override void EquipItem(ItemData itemData)
    {
        WeaponItemData weaponItem = itemData as WeaponItemData;
        if (weaponItem != null)
        {
            PlayerPrefs.SetInt("currrenWeapon", (int)weaponItem.WeaponType);

        }
    }


    //protected override void PreviewItem(ItemData itemData)

    //{
    //    WeaponItemData weaponItem = itemData as WeaponItemData;
    //    //if (weaponItem != null)
    //    //{
    //    //    character.PreviewWeapon(weaponItem.WeaponType);
    //    //}
    //}
}
