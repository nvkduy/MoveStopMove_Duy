using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public abstract class UIShop : UICanvas
{

    [SerializeField] protected Image previewImage;
    [SerializeField] protected Button btnPrev;
    [SerializeField] protected Button btnNext;
    [SerializeField] protected Button btnBuy;
    [SerializeField] protected Button btnTake;
    [SerializeField] protected Button btnBack;
    [SerializeField] protected Character character;


    protected int currentItem = 0;
    protected int maxNumberOfData;
    protected ItemData currentItemData;

    private void Start()
    {
        OnInit();
        
    }
    private void OnInit()
    {
        SetMaxNumberItem();
        btnNext.onClick.AddListener(ChangeNext);
        btnPrev.onClick.AddListener(ChangePrev);
        btnBuy.onClick.AddListener(BuyItem);
        btnTake.onClick.AddListener(TakeItem);
        btnBack.onClick.AddListener(BackShop);
        ChangeItem();
    }

   
    private void ChangePrev()
    {
        if (currentItem > 0)
        {
            currentItem--;
            ChangeItem();
            Debug.Log("p");
        }
    }
    private void ChangeNext()
    {
        if (currentItem < maxNumberOfData - 1)
        {
            currentItem++;
            ChangeItem();
        }
    }

    private void BuyItem()
    {
        if (!currentItemData.isUnlocked && character.Coins >= currentItemData.price)
        {
            character.Coins -= currentItemData.price;
            currentItemData.isUnlocked = true;
            currentItemData.SaveUnlockState();
            UpdateUI();
        }
        else
        {
            Debug.Log("not coins");
        }
    }
    private void TakeItem()
    {
        if (currentItemData.isUnlocked)
        {
            EquipItem(currentItemData);
            UpdateUI();
            Debug.Log("Item equipped: " + currentItemData.itemName);
        }
    }

    private void BackShop()
    {
        Close(0);
        UIManager.Instance.OpenUI<CanvasShop>();
    }

    protected void UpdateUI()
    {
        btnBuy.interactable = !currentItemData.isUnlocked;
        btnTake.interactable = currentItemData.isUnlocked;
    }
    protected void UpdatePreview(Sprite itemSprite)
    {
        if (previewImage != null)
        {
            previewImage.sprite = itemSprite; // Cập nhật ảnh đại diện của item trong ô preview
        }
    }
    
    protected abstract void ChangeItem();
    protected abstract void EquipItem(ItemData itemData);
    protected abstract void SetMaxNumberItem();
    //Preview 3D
    //protected abstract void PreviewItem(ItemData itemData);
}