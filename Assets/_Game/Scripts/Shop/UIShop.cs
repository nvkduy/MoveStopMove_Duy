﻿using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public abstract class UIShop : UICanvas
{
    [SerializeField]
    private WeaponData weaponDataOfNum; 
    [SerializeField] 
    private Image previewImage;
    [SerializeField]
    protected Button btnPrev;
    [SerializeField]
    protected Button btnNext;
    [SerializeField]
    protected Button btnBuy;
    [SerializeField]
    protected Button btnTake;

    protected Character character;


    protected int currentItem = 0;
    protected int maxNumberOfData;
    protected ItemData currentItemData;

    private void Start()
    {
        OnInit();
    }
    public void OnInit()
    {
        character = FindObjectOfType<Character>();
        maxNumberOfData = weaponDataOfNum.Count;
        Open();
    }
    public void ChangePrev()
    {
        if (currentItem > 0)
        {
            currentItem--;
            ChangeItem();
            Debug.Log("p");
        }
    }
    public void ChangeNext()
    {
        if (currentItem < maxNumberOfData - 1)
        {
            currentItem++;
            ChangeItem();
            Debug.Log("n");
        }
    }

 
    
    
    public void BuyItem()
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
    public void TakeItem()
    {
        if (currentItemData.isUnlocked)
        {
            EquipItem(currentItemData);
            UpdateUI();
            Debug.Log("Item equipped: " +  currentItemData.itemName);
        }
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
    public override void Open()
    {
        base.Open();
        btnNext.onClick.AddListener(ChangeNext);
        btnPrev.onClick.AddListener(ChangePrev);
        btnBuy.onClick.AddListener(BuyItem);
        btnTake.onClick.AddListener(TakeItem);
        ChangeItem();
    }
    public abstract void ChangeItem();
    protected abstract void EquipItem(ItemData itemData);
    protected abstract void PreviewItem(ItemData itemData);
}