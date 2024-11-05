using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindState : IState<Bot>
{
    
    public void OnEnter(Bot t)
    {
            
    }

    public void OnExecute(Bot t)
    {
       
        if (t.TargetEnemy != Vector3.zero)
        {
            t.ChangeState(new AttackState());
        }

        else
        {
            if (t.IsDestination)
            {

            }
           
            t.FindToEnemy();
            t.ChangeAnim(Constants.RUN_ANIM_NAME);
            t.BotMove();
        }
        
    }

    public void OnExit(Bot t)
    {
       
    }
}
