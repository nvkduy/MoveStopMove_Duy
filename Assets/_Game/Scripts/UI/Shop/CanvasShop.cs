using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasShop : UICanvas
{
    [SerializeField] Button btnWeaponShop;
    [SerializeField] Button btnSkinShop;
    [SerializeField] Button btnBack;

    public override void Setup()
    {
        base.Setup();
        btnWeaponShop.onClick.AddListener(WeaponButton);
        btnSkinShop.onClick.AddListener(SkinButton);
        btnBack.onClick.AddListener(BackButton);
    }
    public void WeaponButton()
    {
        Close(0);
        UIManager.Instance.OpenUI<CanvasWeaponShop>();

    }
    public void SkinButton()
    {
        Close(0);
        UIManager.Instance.OpenUI<CanvasSkinShop>();
    }
    public void BackButton()
    {
        Close(0);
        UIManager.Instance.OpenUI<CanvasMainMenu>();
    }
}
