using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CanvasSettings : UICanvas
{
    [SerializeField] GameObject[] buttons;
    [SerializeField] Button btnContinue;
    [SerializeField] Button btnClose;
    [SerializeField] Button btnMainMenu;

    public override void Setup()
    {
        base.Setup();
        btnContinue.onClick.AddListener(ContinueButton);
        btnClose.onClick.AddListener(CloseButton);
        btnMainMenu.onClick.AddListener(MainMenuButton);
    }
    public void SetState(UICanvas canvas)
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].gameObject.SetActive(false);
        }
        if (canvas is CanvasMainMenu)
        {
            buttons[2].gameObject.SetActive(true);

        }
        
        else if (canvas is CanvasGamePlay)
        {     
            buttons[0].gameObject.SetActive(true);
            
            buttons[1].gameObject.SetActive(true);
            

        }

    }
    //0-1-2 : mainmenu-continue-close
    public void ContinueButton()
    {
        Close(0);
    }

    public void CloseButton()
    {
        Close(0);
        UIManager.Instance.OpenUI<CanvasMainMenu>();
    }
    public void MainMenuButton()
    {
        UIManager.Instance.CloseAll();
        UIManager.Instance.OpenUI<CanvasMainMenu>();
    }
}
