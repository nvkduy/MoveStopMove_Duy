using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasFail : UICanvas
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] Button btnMainMenu;
    [SerializeField] Button btnRetry;

    public override void Setup()
    {
        base.Setup();
        btnMainMenu.onClick.AddListener(MainMenuButton);
        btnRetry.onClick.AddListener(RetryButton);


    }
    public void SetBestScore(int score)
    {
        scoreText.text = score.ToString();
    }
    public void MainMenuButton()
    {
        UIManager.Instance.CloseAll();
        LevelManager.Instance.OnFinshGame();
        UIManager.Instance.OpenUI<CanvasMainMenu>();
    }
    public void RetryButton()
    {
        Close(0);
        LevelManager.Instance.RetryLevel();
    }
    
}
