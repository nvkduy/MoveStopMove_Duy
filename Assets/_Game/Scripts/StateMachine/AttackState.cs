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
        t.MoveStop();
        t.OnAttack();
    }

    public void OnExit(Bot t)
    {

    }

}
