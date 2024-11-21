using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasPantsShop : UIShop
{
    [SerializeField] PantData pantData;

    protected override void SetMaxNumberItem()
    {
        maxNumberOfData = pantData.CountPant;
    }
    protected override void ChangeItem()
    {
        currentItemData = pantData.GetPant((PantsType)currentItem);
        UpdateUI();
        UpdatePreview(currentItemData.icon);
    }

    protected override void EquipItem(ItemData itemData)
    {
        PantItemData pantItem = itemData as PantItemData;
        if (pantItem != null)
        {
            PlayerPrefs.SetInt("currrenPant", (int)pantItem.PantsType);

        }
    }
}
