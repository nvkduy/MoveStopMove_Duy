using UnityEngine;

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

        var playerPos = LevelManager.Instance.Player;
        if (playerPos != null)
        {
            Vector3 centerPos = playerPos.transform.position;
            Vector3 point;
            if (t.RandomPoint(centerPos,5f, out point))
            {
                t.SetDestination(point);
            }
        }

    }
}
