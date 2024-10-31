using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindState : IState<Bot>
{
    private int brickNumber;
    private int currentBrick;
    public void OnEnter(Bot t)
    {
        brickNumber = Random.Range(5,8);
        
    }

    public void OnExecute(Bot t)
    {
        if (currentBrick < brickNumber)
        {

        }
        Debug.Log("FindState");
    }

    public void OnExit(Bot t)
    {
       
    }
}
