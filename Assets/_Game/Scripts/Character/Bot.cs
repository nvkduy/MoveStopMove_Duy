using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
    [SerializeField] float speedMove;

    public float range = 10.0f;
    public NavMeshAgent agent;

    private Vector3 target;
    private IState<Bot> currentState;
    private bool isMoveBot= true;
    protected virtual void Start()
    {
        OnInit();
        agent.stoppingDistance = 0.01f;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState != null)
        {
            currentState.OnExecute(this);
        }
     
    }
    private void OnInit()
    {
        ChangeState(new IdleState());

    }
    public void ChangeState(IState<Bot> state)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }

        currentState = state;

        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }
    public void FindAndAttack()
    {
        if (isMoveBot)
        {
            Vector3 point;
            if (RandomPoint(transform.position, range, out point))
            {
                target = point;
                Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);
                BotMove(target);
                isMoveBot = false;
               
            }

        }else if(targetEnemy != Vector3.zero && !isAttack)
        {
            Attack();
            isAttack = false;

            if (resetAttackCoroutine == null)
            {
                resetAttackCoroutine = StartCoroutine(ResetAttack());
            }
        }    

        if (isMoveBot == false && agent.remainingDistance <= agent.stoppingDistance)
        {
            isMoveBot= true;
        }


    }
    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }
    public void BotMove(Vector3 target)
    {
        if (target != Vector3.zero)
        {
            agent.SetDestination(target * Time.deltaTime);
        }
    }
}
