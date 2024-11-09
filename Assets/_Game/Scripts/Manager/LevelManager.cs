using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] Bot bot;
    [SerializeField] Player player;
    [SerializeField] Bot botPrefab;
    public int CharacterAmount => currentLevel.botAmount + 1;
    public Level[] levelPrefab;
    
    private List<Bot> bots = new List<Bot>();
    private Level currentLevel;
    private int levelIndex;
  

    private void Awake()
    {
        
        levelIndex = PlayerPrefs.GetInt("Level", 0);
    }

    void Start()
    {
        
        OnStartGame();
        OnInit();
    }

  public void OnInit()
    {
        //init vị trí bắt đầu game
        Vector3 index = currentLevel.startPoint.position;
        Vector3 point;
        List<Vector3> startPoints = new List<Vector3>();
        for (int i = 0; i < CharacterAmount; i++)
        {
            if (bot.RandomPoint(index, bot.range, out point))
            {
                startPoints.Add(point+Vector3.right*i);

            }

        }

        //Set vị trí player
        int rand = Random.Range(0, CharacterAmount);
        player.transform.position = startPoints[rand];
        startPoints.RemoveAt(rand);
        player.OnInit();

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
        LoadLevel(levelIndex);
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
    internal void NextLevel(int level)
    {
        levelIndex++;
        PlayerPrefs.SetInt("Level", levelIndex);
        OnReset();
        LoadLevel(levelIndex);

    }
    public void RemoveBots()
    {
        if (bots.Count > 0)
        {
            Bot remove = bots[bots.Count - 1];
            bots.Remove(remove);
        }
        
    }

    internal void OnRetry()
    {
        OnReset();
        LoadLevel(levelIndex);
        OnInit();
        //UIManager.Instance.OpenUI<MainMenu>();
    }

}
