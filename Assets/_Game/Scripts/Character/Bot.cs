using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
    [SerializeField] float speedMove;
    [SerializeField] float range = 10.0f;
    [SerializeField] NavMeshAgent agent;

    private IState<Bot> currentState;
    private bool isMoveBot= true;
    private Vector3 destionation;
    

    public Vector3 TargetEnemy
    {
        get { return targetEnemy; }
    }
    public bool IsDestination => Vector3.Distance(destionation, Vector3.right * transform.position.x + Vector3.forward * transform.position.z) < 0.1f;
    protected virtual void Start()
    {
        OnInit();
        
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
        agent.stoppingDistance = 0.01f;
        ChangeWeapon(WeaponType.Boomerang);

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
    public void BotMove()
    {
        if (isMoveBot)
        {
            Vector3 point;
            if (RandomPoint(transform.position, range, out point))
            {
                
                Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);
                SetDestination(point);
                isMoveBot = false;
            }
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
        result = transform.position;
        return false;
    }
   
    public void SetDestination(Vector3 position)
    {
        agent.enabled = true;
        destionation = position;
        destionation.y = 0;
        agent.SetDestination(position);
    }
    public void FindToEnemy()
    {
        base.FindEnemy(transform.position,radius);
    }
    public void OnAttack()
    {
        base.Attack();
    }
    //public IEnumerator ColldowAttack()
    //{
    //    base.ResetAttack();
    //}
    //public void onattack()
    //{
    //    if (targetenemy != vector3.zero && !isattack)
    //    {
    //        changeanim(constants.attack_anim_name);
    //        base.attack();
    //        isattack = true;
    //        if (resetattackcoroutine == null)
    //        {
    //            resetattackcoroutine = startcoroutine(resetattack());
    //        }
    //    }

    //}
    public void MoveStop()
    {
        agent.enabled = false;
    }
   
}
