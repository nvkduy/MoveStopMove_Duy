using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Bot : Character
{
    [SerializeField] private float speedMove;
    public float range = 10.0f;

    private IState<Bot> currentState;
    private Vector3 destination;
    private WeaponType weaponType;
    private HatsType hatType;
    private PantsType pantsType;
    public Vector3 TargetEnemy => targetEnemy;

    public bool IsDestination => Vector3.Distance(new Vector3(destination.x, TF.position.y, destination.z), TF.position) < 1f;

    private void Update()
    {
        currentState?.OnExecute(this);
    }

    public void OnInit()
    {
       
        ChangeState(new FindState());
        weaponType = (WeaponType)Random.Range(0, 3);
        hatType = (HatsType)Random.Range(0, 3);
        pantsType = (PantsType)Random.Range(0, 3);
        ChangeWeapon(weaponType);
        ChangeHat(hatType);
        ChangePant(pantsType);
        transform.localScale = Vector3.one;
        radius = 2f;
        currentTime = 0;
        targetEnemy = Vector3.zero;

        Debug.Log($"Bot initialized with weapon: {weaponType}, hat: {hatType}, pants: {pantsType}");
    }

    public void Init()
    {
        transform.localScale = Vector3.one;
        radius = 2f;
        currentTime = 0;
        targetEnemy = Vector3.zero;

    }

    public void ChangeState(IState<Bot> state)
    {
        if (GameManager.Instance.IsState(GameState.GamePlay))
        {
            currentState?.OnExit(this);
            currentState = state;
            currentState?.OnEnter(this);
        }
    }

    public bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 10; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            if (NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, 1.0f, NavMesh.AllAreas))
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
        destination = position;
        destination.y = 0;
        agent.SetDestination(position);
    }

    public void FindToEnemy()
    {
        base.FindEnemy(transform.position, radius);
    }

    public void OnAttack()
    {
        agent.enabled = false;
        base.Attack();
    }

    public void MoveStop()
    {
        agent.enabled = false;
    }

    protected override void Die()
    {
        base.Die();
        agent.enabled = false;
        LevelManager.Instance.RemoveBots();
    }
}
