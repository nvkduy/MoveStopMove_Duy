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
    List<Vector3> startPoints = new List<Vector3>();
    private int levelIndex;
    private int currentBotAmount;
    private int maxCurrentBotAmount = 5;
    private int botAppearAmount;

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

        // Initialize starting positions
        Vector3 index = currentLevel.startPoint.position;
        Vector3 point;

        int retries = CharacterAmount; // Number of retries to generate points

        for (int i = 0; i <= CharacterAmount; i++)
        {
            bool pointGenerated = false;
            int retryCount = 0;
            while (!pointGenerated && retryCount < retries)
            {
                if (bot.RandomPoint(index, 10, out point))
                {
                    startPoints.Add(point);
                    pointGenerated = true;
                }
                retryCount++;
            }
            if (!pointGenerated)
            {
                Debug.LogError($"Failed to generate point for character {i} after {retryCount} retries.");
            }
        }

        if (startPoints.Count < CharacterAmount)
        {
            Debug.LogError("Not enough start points generated.");
            return;
        }

        // Set player position and spawn player
        SpawnPlayer();

        // Set bot positions and spawn bots
        SpawnBot();
    }

    private void SpawnPlayer()
    {
        int rand = UnityEngine.Random.Range(0, CharacterAmount);
        player = SimplePool.Spawn<Player>(PoolType.Player, startPoints[rand], Quaternion.identity);
        player.transform.position = startPoints[rand];
        startPoints.RemoveAt(rand);
        player.OnInit();
        PlayerTF?.Invoke(player.transform);
    }

    private void SpawnBot()
    {
        while (currentBotAmount < maxCurrentBotAmount && botAppearAmount < CharacterAmount - 1)
        {
            int ranPos = UnityEngine.Random.Range(0, startPoints.Count);
            Vector3 spawnPos = startPoints[ranPos];
            bot = SimplePool.Spawn<Bot>(PoolType.Bot, spawnPos, Quaternion.identity);
            bot.OnInit();
            bots.Add(bot);
            botAppearAmount++;
            currentBotAmount++;
        }
    }
    public void OnStartGame()
    {
        GameManager.Instance.ChangeState(GameState.GamePlay);
        for (int i = 0; i < bots.Count; i++)
        {
            bots[i].ChangeState(new FindState());
        }
    }

    public void OnFinshGame()
    {
        OnReset();
        Destroy(currentLevel.gameObject);
    }

    public void LoadLevel(int level)
    {
        if (currentLevel != null)
        {
            Destroy(currentLevel.gameObject);
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
        bots.Clear();
        currentBotAmount = 0;
        botAppearAmount = 0;
        startPoints.Clear();

    }

    internal void NextLevel()
    {
        levelIndex++;
        if (levelIndex >= levelPrefab.Count)
        {
            levelIndex = 0; // Quay lại level đầu tiên nếu vượt quá số lượng level
        }
        PlayerPrefs.SetInt("Level", levelIndex);
        OnReset();
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
            SpawnBot();
        }
    }

    internal void RetryLevel()
    {
        OnReset();
        LoadLevel(levelIndex);
        OnInit();
        UIManager.Instance.OpenUI<CanvasGamePlay>();
    }
}
