using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class FindState : IState<Bot>
{

    public void OnEnter(Bot t)
    {
        SeekTarget(t);
    }

    public void OnExecute(Bot t)
    {

        if (t.TargetEnemy != Vector3.zero)
        {
            t.ChangeState(new AttackState());

        }
        else if (t.IsDestination)
        {
            SeekTarget(t);
        }
        t.FindToEnemy();
        t.currentTime -= Time.deltaTime;

    }

    public void OnExit(Bot t)
    {

    }
    private void SeekTarget(Bot t)
    {
        Vector3 point;
        if (t.RandomPoint(t.transform.position, t.range, out point))
        {
            t.ChangeAnim(Constants.RUN_ANIM_NAME);
            Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);
            t.SetDestination(point);
        }
    }
}
