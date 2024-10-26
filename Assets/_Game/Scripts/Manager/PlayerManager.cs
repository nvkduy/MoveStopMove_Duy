using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
   private int playerCount = 50;
   public int KillCount { get; private set; }
   public int KillNumber()
    {
        KillCount++;
        Debug.Log(KillCount);
        return KillCount;

    }
 
}
