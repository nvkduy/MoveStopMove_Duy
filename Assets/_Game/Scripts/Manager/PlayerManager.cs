using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
   private int playerCount = 50;
   public int KillCount { get; private set; }
   public void KillNumber()
    {
        KillCount++;
        Debug.Log(KillCount);
    }
 
}
