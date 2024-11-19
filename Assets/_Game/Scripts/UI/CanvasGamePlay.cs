using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasGamePlay : UICanvas
{
    [SerializeField] TextMeshProUGUI coinText;
    [SerializeField] Joystick joystick;
    public Joystick GetJoytick()
    {
        return joystick;
    }
    public override void Setup()
    {
        base.Setup();
        UpdateCoin(0);
    }
    public void UpdateCoin(int coin)
    {
        coinText.text = coin.ToString();
    }
    public void SettingsButton()
    {
        UIManager.Instance.OpenUI<CanvasSettings>().SetState(this);
    }

}