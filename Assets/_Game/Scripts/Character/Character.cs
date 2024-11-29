using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.AI;

public class Character : GameUnit
{
    public int numOfEnemy;
    protected Weapon currentWeapon;
    [SerializeField] internal Transform weaponParent;
    [SerializeField] protected Transform hatParent;
    [SerializeField] protected float radius;
    [SerializeField] protected NavMeshAgent agent;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private Animator animator;
    [SerializeField] private SkinnedMeshRenderer skinnedMeshRenderer;

    internal Vector3 targetEnemy;
    internal float currentTime = 0;

    protected bool isAttack = false;

    private Collider[] hitColliders = new Collider[20];
    private string currentAnim;
    private Hats currentHat;

    public int Coins { get; set; } = 1000;

    protected void FindEnemy(Vector3 position, float radius)
    {
        numOfEnemy = Physics.OverlapSphereNonAlloc(position, radius, hitColliders, enemyLayer);
        float minDistance = Mathf.Infinity;
        Collider nearestEnemy = null;

        for (int i = 0; i < numOfEnemy; i++)
        {
            if (hitColliders[i].gameObject != this.gameObject)
            {
                float distance = Vector3.Distance(position, hitColliders[i].transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestEnemy = hitColliders[i];
                }
            }
        }
        if (nearestEnemy != null)
        {
            targetEnemy = nearestEnemy.transform.position;
        }
        if (numOfEnemy == 1)
        {
            targetEnemy = Vector3.zero;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
        if (hitColliders != null)
        {
            for (int i = 0; i < numOfEnemy; i++)
            {
                if (hitColliders[i] != null)
                {
                    Gizmos.DrawLine(transform.position, hitColliders[i].transform.position);
                }
            }
        }
    }

    protected void Attack()
    {
        if (currentWeapon != null && isAttack && currentTime <= 0)
        {
            ChangeAnim(Constants.ATTACK_ANIM_NAME);
            currentWeapon.Throw(this, OnHitVicTim); // Gọi phương thức với tham số            
            currentTime = 3f;
        }
    }

    protected virtual void CanAttack()
    {
       StartCoroutine(CanAtack());
    }
    IEnumerator CanAtack()
    {
        yield return new WaitForSeconds(4f);
        isAttack = true;
    }
    protected virtual void OnHitVicTim(Character attacker, Character victim)
    {
        if (victim != null && attacker != victim)
        {
            victim.Die();
            attacker.UpSize();
           
            Debug.Log("victim :" + victim.name + ", Attacker :" + attacker.name);
        }
        
    }

    protected virtual void Die()
    {
        ChangeAnim(Constants.DIE_ANIM_NAME);
        SimplePool.Despawn(this);
        
        if (LevelManager.Instance.CountOfBot == 1)
        {
            UIManager.Instance.OpenUI<CanvasVictory>();
        }
    }

    protected void UpSize()
    {
        transform.localScale += Vector3.one * 0.5f;
        radius += 1f;
    }

    public void ChangeWeapon(WeaponType weaponType)
    {
        // Hide the old weapon if it exists
        if (currentWeapon != null)
        {
            SimplePool.Despawn(currentWeapon);
            currentWeapon = null;
        }
        Weapon wp = DataManager.Instance.GetWeapon(weaponType);
        // Spawn the new weapon
        currentWeapon = SimplePool.Spawn<Weapon>(wp, weaponParent);
       // currentWeapon = Instantiate(wp, weaponParent);  
        currentWeapon.transform.localScale = wp.transform.localScale;
    }

    public void ChangeHat(HatsType hatsType)
    {
        // Hide the old hat if it exists
        if (currentHat != null)
        {
            SimplePool.Despawn(currentHat);
            currentHat = null;
        }
        Hats hat = DataManager.Instance.GetHat(hatsType);
        // Spawn the new hat
        currentHat = SimplePool.Spawn<Hats>(hat, hatParent);
    }

    public void ChangePant(PantsType pantType)
    {
        Material pantMaterial = DataManager.Instance.GetPant(pantType);
        skinnedMeshRenderer.material = pantMaterial;
    }

 
    public void ChangeAnim(string animName)
    {
        if (currentAnim != animName)
        {
            animator.ResetTrigger(animName);
            currentAnim = animName;
            animator.SetTrigger(currentAnim);
        }
    }
}
