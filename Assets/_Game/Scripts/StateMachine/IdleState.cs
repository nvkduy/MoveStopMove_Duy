using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IdleState : IState<Bot>
{
    

    private float currentTime;

    public void OnEnter(Bot t)
    {
        currentTime = 0f;
    }

    public void OnExecute(Bot t)
    {
        if (currentTime < 2f)
        {
            t.ChangeAnim(Constants.IDLE_ANIM_NAME);
            t.MoveStop();
        }
        else
        {
            t.ChangeState(new FindState());
        }
        currentTime += Time.deltaTime;
    }

    public void OnExit(Bot t)
    {
       
    }
    
   
}
