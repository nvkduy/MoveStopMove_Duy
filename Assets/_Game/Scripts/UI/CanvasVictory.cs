using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasVictory : UICanvas
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] Button btnMainMenu;
    [SerializeField] Button btnNextLevel;

    public override void Setup()
    {
        base.Setup();
        btnMainMenu.onClick.AddListener(MainMenuButton);
        btnNextLevel.onClick.AddListener(NextLevelButton);
    }
    public void SetBestScore(int score)
    {
        scoreText.text = score.ToString();
    }
    public void MainMenuButton()
    {
        UIManager.Instance.CloseAll();
        UIManager.Instance.OpenUI<CanvasMainMenu>();   
    }

    public void NextLevelButton()
    {
        UIManager.Instance.CloseAll();
        LevelManager.Instance.NextLevel();
    }
}