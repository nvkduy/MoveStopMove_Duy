using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState<Bot>
{
    public void OnEnter(Bot t)
    {
       
    }

    public void OnExecute(Bot t)
    {
        if (t.targetEnemy == Vector3.zero)
        {
           t.ChangeState(new FindState());
        }
        else if(t.currentTime <=0)
        {
            
            t.MoveStop();
            t.OnAttack();
           
        }
        t.FindToEnemy();
        t.currentTime -=Time.deltaTime;
    }

    public void OnExit(Bot t)
    {
        
    }

}
