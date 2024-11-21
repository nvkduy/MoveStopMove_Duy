using UnityEngine;

public class CanvasHatsShop : UIShop
{
    [SerializeField] HatsData hatsData;

    protected override void SetMaxNumberItem()
    {
        maxNumberOfData = hatsData.CountHats;
    }
    protected override void ChangeItem()
    {
        currentItemData = hatsData.GetHat((HatsType)currentItem);
        UpdateUI();
        UpdatePreview(currentItemData.icon);
    }

    protected override void EquipItem(ItemData itemData)
    {
        HatsItemData hatItem = itemData as HatsItemData;
        if (hatItem != null)
        {
            PlayerPrefs.SetInt("currrenHat", (int)hatItem.HatType);

        }
    }
}
