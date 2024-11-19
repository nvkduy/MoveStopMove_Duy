using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasMainMenu : UICanvas
{
    public void PlayButton()
    {
        Close(0);
        //Instance key để truy cập tới đối tượng duy nhất của lớp UIManager mà không cần phải tạo mới mỗi lần muốn sử dụng.
        CanvasGamePlay playCanve = UIManager.Instance.OpenUI<CanvasGamePlay>();
        
        LevelManager.Instance.LoadLevel(0);
        LevelManager.Instance.OnInit();
        LevelManager.Instance.OnStartGame();
        LevelManager.Instance.Player.Joystick = playCanve.GetJoytick();
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