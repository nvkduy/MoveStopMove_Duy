using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasGamePlay : UICanvas
{
    [SerializeField] TextMeshProUGUI coinText;
    [SerializeField] Joystick joystick;
    [SerializeField] Button btnSetting;
    public Joystick GetJoytick()
    {
        return joystick;
    }
    public override void Setup()
    {
        base.Setup();
        btnSetting.onClick.AddListener(SettingsButton);
        UpdateCoin(0);
    }
    public void UpdateCoin(int coin)
    {
        coinText.text = coin.ToString();
    }
    public void SettingsButton()
    {
        Close(0);
        UIManager.Instance.OpenUI<CanvasSettings>().SetState(this);
    }
    

}