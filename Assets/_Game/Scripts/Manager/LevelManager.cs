using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] Bot bot;
    [SerializeField] Player player;
    [SerializeField] Bot botPrefab;
    public int CharacterAmount => currentLevel.botAmount + 1;
    public Level[] levelPrefab;
    public List<Bot> bots = new List<Bot>();
    public Player Player { get { return player; } }

    private Level currentLevel;
    private int levelIndex;
    public Action<Transform> PlayerTF;
    public int CountOfBot => bots.Count;
    private void Awake()
    {
        //  UIManager.Instance.OnLoad += OnInit;
        
    }

    private void Start()
    {
        UIManager.Instance.OpenUI<CanvasMainMenu>();
    }

    public void OnInit()
    {
        
        levelIndex = PlayerPrefs.GetInt("Level", 0);
        //init vị trí bắt đầu game
        Vector3 index = currentLevel.startPoint.position;
        Vector3 point;
        List<Vector3> startPoints = new List<Vector3>();
        for (int i = 0; i <= CharacterAmount; i++)
        {
            if (bot.RandomPoint(index, bot.range, out point))
            {
                startPoints.Add(point+Vector3.right*i);

            }

        }

        //Set vị trí player
        int rand = UnityEngine.Random.Range(0, CharacterAmount);
        player.transform.position = startPoints[rand];
        player = SimplePool.Spawn<Player>(player, startPoints[rand],Quaternion.identity);
        startPoints.RemoveAt(rand);
        player.OnInit();
        PlayerTF?.Invoke(player.transform);

        //Set vị trí bot
        for (int i = 0; i < CharacterAmount - 2; i++)
        {
            bot = SimplePool.Spawn<Bot>(botPrefab, startPoints[i],Quaternion.identity);
            bot.OnInit();
            bots.Add(bot);
        }
    }

    public void OnStartGame()
    {
        GameManager.Instance.ChangeState(GameState.Gameplay);
        for (int i = 0; i < bots.Count; i++)
        {
            bots[i].ChangeState(new FindState());
        }
    }
    public void OnFinshGame()
    {

    }
    public void LoadLevel(int level)
    {
        if (currentLevel != null)
        {
            Destroy(currentLevel.gameObject);
        }
        else if (level < levelPrefab.Length)
        {
            PlayerPrefs.SetInt("Level", levelIndex);
            currentLevel = Instantiate(levelPrefab[level]);
        }
    }
    public void OnReset()
    {
        SimplePool.CollectAll();
        bots.Clear();
    }
    internal void NextLevel()
    {
        levelIndex++;
        PlayerPrefs.SetInt("Level", levelIndex);
        OnReset();
        LoadLevel(levelIndex);
        OnInit();

    }
    public void RemoveBots()
    {
        if (bots.Count > 0)
        {
            Bot remove = bots[bots.Count - 1];
            bots.Remove(remove);
        }
        
    }

    internal void RetryLevel()
    {
        OnReset();
        LoadLevel(levelIndex);
        OnInit();
        UIManager.Instance.OpenUI<CanvasMainMenu>();
    }

}
