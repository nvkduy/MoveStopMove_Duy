using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasMainMenu : UICanvas
{
    [SerializeField] Button btnPlay;
    [SerializeField] Button btnSetting;
    [SerializeField] Button btnShop;
    public override void Setup()
    {
        base.Setup();
        btnPlay.onClick.AddListener(PlayButton);
        btnSetting.onClick.AddListener(SettingButton);
        btnShop.onClick.AddListener(ShopButton);

    }
    public void PlayButton()
    {
        Close(0);
        //Instance key để truy cập tới đối tượng duy nhất của lớp UIManager mà không cần phải tạo mới mỗi lần muốn sử dụng.
        CanvasGamePlay playCanvas = UIManager.Instance.OpenUI<CanvasGamePlay>();
        
        LevelManager.Instance.LoadLevel(LevelManager.Instance.LevelIndex);
        LevelManager.Instance.OnInit();
        LevelManager.Instance.OnStartGame();
        LevelManager.Instance.Player.Joystick = playCanvas.GetJoytick();

    }

    public void SettingButton()
    {
        Close(0);
        UIManager.Instance.OpenUI<CanvasSettings>().SetState(this);
    }
    public void ShopButton()
    {
        Close(0);
        UIManager.Instance.OpenUI<CanvasShop>();
    }
}