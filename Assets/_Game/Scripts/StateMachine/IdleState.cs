using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IdleState : IState<Bot>
{
    

    private float currentTime;

    public void OnEnter(Bot t)
    {
       t.ChangeAnim(Constants.DIE_ANIM_NAME);
    }

    public void OnExecute(Bot t)
    {
       
    }

    public void OnExit(Bot t)
    {
       
    }
    
   
}
