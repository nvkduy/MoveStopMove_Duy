using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasShop : UICanvas
{
    public void WeaponButton()
    {
        Close(0);
        UIManager.Instance.OpenUI<WeaponShop>();

    }
    public void SkinButton()
    {
        Close(0);

    }
    public void Back()
    {
        Close(0);
        UIManager.Instance.OpenUI<CanvasMainMenu>();
    }
}
