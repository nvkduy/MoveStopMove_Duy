using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private Bot bot;
    [SerializeField] private Player player;
    public List<Level> levelPrefab;
    public Action<Transform> PlayerTF;
    public List<Bot> bots = new List<Bot>();

    private Level currentLevel;
    private List<Vector3> startPoints = new List<Vector3>();
    private int levelIndex;
    private int currentBotAmount;
    private const int maxCurrentBotAmount = 5;
    private int botAppearAmount;
    private bool isInitialized = false;
    public int LevelIndex => levelIndex;
    public int CountOfBot => bots.Count;
    public int BotAppearAmount => botAppearAmount;
    public int CharacterAmount => currentLevel.botAmount + 1;
    public Player Player => player;

    private void Start()
    {
        levelIndex = PlayerPrefs.GetInt("Level", 0);
        UIManager.Instance.OpenUI<CanvasMainMenu>();
    }

    public void OnInit()
    {
        if (currentLevel == null)
        {
            Debug.LogError("Current level is not loaded.");
            return;
        }

        Vector3 pos = currentLevel.startPoint.position;
        startPoints.Clear();

        for (int i = 0; i < CharacterAmount; i++)
        {
            if (!GenerateStartPoint(pos, out Vector3 point))
            {
                Debug.LogWarning($"Failed to generate point for character {i}.");
            }
            else
            {
                startPoints.Add(point);
            }
        }

        if (startPoints.Count < CharacterAmount - 1)
        {
            Debug.LogError("Not enough start points generated.");
            return;
        }

        SpawnPlayer();
        SpawnBot();
    }

    private bool GenerateStartPoint(Vector3 pos, out Vector3 point)
    {
        int retries = CharacterAmount ;
        HashSet<Vector3> attemptedPoints = new HashSet<Vector3>();

        for (int retryCount = 0; retryCount < retries; retryCount++)
        {
            if (bot.RandomPoint(pos, 10, out point) && !attemptedPoints.Contains(point))
            {
                return true;
            }
            attemptedPoints.Add(point);
        }

        point = Vector3.zero;
        return false;
    }

    private void SpawnPlayer()
    {
        int rand = UnityEngine.Random.Range(0, CharacterAmount);
        player = SimplePool.Spawn<Player>(PoolType.Player, startPoints[rand], Quaternion.identity);
        player.OnInit();
        PlayerTF?.Invoke(player.transform);
    }

    private void SpawnBot()
    {
        while (currentBotAmount < maxCurrentBotAmount && botAppearAmount < CharacterAmount - 1)
        {
            int ranPos = UnityEngine.Random.Range(0, startPoints.Count);
            Vector3 spawnPos = startPoints[ranPos];
            Bot Bot = SimplePool.Spawn<Bot>(PoolType.Bot, spawnPos, Quaternion.identity);
            Bot.OnInit();
            bots.Add(Bot);
            botAppearAmount++;
            currentBotAmount++;
        }
    }

    private void NewSpawnBot()
    {
        while (currentBotAmount < maxCurrentBotAmount && botAppearAmount < CharacterAmount - 1)
        {
            int ranPos = UnityEngine.Random.Range(0, startPoints.Count);
            Vector3 spawnPos = startPoints[ranPos];
            Bot newBot = SimplePool.Spawn<Bot>(PoolType.Bot, spawnPos, Quaternion.identity);
            newBot.Init();
            bots.Add(newBot);
            botAppearAmount++;
            currentBotAmount++;
        }
    }
    public void OnStartGame()
    {
        GameManager.Instance.ChangeState(GameState.GamePlay);
        foreach (var bot in bots)
        {
            bot.ChangeState(new FindState());
        }
    }

    public void OnFinshGame()
    {
        OnReset();
        Destroy(currentLevel.gameObject);
        Resources.UnloadUnusedAssets();
    }

    public void LoadLevel(int level)
    {
        if (currentLevel != null)
        {
            Destroy(currentLevel.gameObject);
            Resources.UnloadUnusedAssets();
        }
        if (level < levelPrefab.Count)
        {
            PlayerPrefs.SetInt("Level", level);
            currentLevel = Instantiate(levelPrefab[level]);
        }
    }

    public void OnReset()
    {
        SimplePool.CollectAll();
        SimplePool.CollectAllWeapons();
        bots.Clear();
        currentBotAmount = 0;
        botAppearAmount = 0;
    }

    internal void NextLevel()
    {
        levelIndex = (levelIndex + 1) % levelPrefab.Count;
        PlayerPrefs.SetInt("Level", levelIndex);
        OnReset();
        bots.Clear();
        LoadLevel(levelIndex);
        OnInit();
        UIManager.Instance.OpenUI<CanvasGamePlay>();
    }

    public void RemoveBots()
    {
        if (bots.Count > 0)
        {
            Bot remove = bots[bots.Count - 1];
            bots.Remove(remove);
            currentBotAmount--;
            NewSpawnBot();
        }
    }

    internal void RetryLevel()
    {
        OnReset();
        LoadLevel(levelIndex);
        SpawnPlayer();
        NewSpawnBot();
    }
}
