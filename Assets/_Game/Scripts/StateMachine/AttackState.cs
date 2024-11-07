using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState<Bot>
{
    public void OnEnter(Bot t)
    {
        t.ChangeAnim(Constants.ATTACK_ANIM_NAME);
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

    }

    public void OnExit(Bot t)
    {
        
    }

}
