using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Level : MonoBehaviour
{
    [SerializeField] internal Transform startPoint;
    [SerializeField] internal int botAmount;
    [SerializeField] private Bot bot;
    

    // method to random point navmesh
    public void OnInit()
    {
        Player player = LevelManager.Instance.Player;
        // Initialize starting positions
        Vector3 index = startPoint.position;
        Vector3 point;
        List<Vector3> startPoints = new List<Vector3>();
        int retries = 10; // Number of retries to generate points

        for (int i = 0; i <= botAmount; i++)
        {
            bool pointGenerated = false;
            for (int retry = 0; retry < retries; retry++)
            {
                if (bot.RandomPoint(index, bot.range, out point))
                {
                    startPoints.Add(point);
                    pointGenerated = true;
                    break;
                }
            }
            if (!pointGenerated)
            {
                Debug.LogError($"Failed to generate point for character {i} after {retries} retries.");
            }
        }

        if (startPoints.Count <= botAmount)
        {
            Debug.LogError("Not enough start points generated.");
            return;
        }

        // Set player position
        int rand = UnityEngine.Random.Range(0, botAmount);
        player = SimplePool.Spawn<Player>(PoolType.Player, startPoints[rand], Quaternion.identity);
        player.transform.position = startPoints[rand];
        startPoints.RemoveAt(rand);
        player.OnInit();
        LevelManager.Instance.PlayerTF?.Invoke(player.transform);

        // Set bot positions
        for (int i = 0; i < botAmount; i++)
        {
            bot = SimplePool.Spawn<Bot>(PoolType.Bot, startPoints[i], Quaternion.identity);
            bot.OnInit();
            LevelManager.Instance.bots.Add(bot);
        }
    }
}
