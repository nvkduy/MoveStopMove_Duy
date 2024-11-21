using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Bot : Character
{
    [SerializeField] float speedMove;
    
    
    public float range = 10.0f;

    private IState<Bot> currentState;
    private Vector3 destionation;
    

    public WeaponType weaponType {  get; private set; }
    public Vector3 TargetEnemy
    {
        get { return targetEnemy; }
    }
  
   public bool IsDestination => Vector3.Distance(new Vector3(destionation.x, TF.position.y, destionation.z),TF.position) < 1f;
    
    // Update is called once per frame
    void Update()
    {
        if (currentState != null)
        {
            currentState.OnExecute(this);
        }
     
    }
    public void OnInit()
    {
        ChangeState(new FindState());
        weaponType =(WeaponType) Random.Range(0, 3);
        ChangeWeapon(weaponType);

    }
    public void ChangeState(IState<Bot> state)
    {
        if (GameManager.Instance.IsState(GameState.GamePlay) && currentState != null)
        {
            currentState.OnExit(this);
        }

        currentState = state;

        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }

    public bool RandomPoint(Vector3 center, float range, out Vector3 result)
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
        result = TF.position;
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
        agent.enabled =false;
        base.Attack();
    }

    public void MoveStop()
    {
        agent.enabled = false;
    }
    public override void Die()
    {
        base.Die();
        agent.enabled = false;
        LevelManager.Instance.RemoveBots();
    }
    
   
}
