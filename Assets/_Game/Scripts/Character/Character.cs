using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] protected float radius;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] Transform weaponParent;

    
    public Weapon currentWeapon;

    internal Vector3 targetEnemy;

    protected float currentTime = 0;
    public int numOfEnemy;
    protected bool isAttack = false;

    Collider[] hitColliders = new Collider[20];
    private string currentAnim;
    




    //private void Start()
    //{
    //    OnInit();
    //}
    public virtual void OnInit()
    {
        
    }
    public void FindEnemy(Vector3 position, float radius)
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

            Debug.Log("Enemy gần nhất: " + nearestEnemy.name);
        }
        if (numOfEnemy == 1)
        {
            targetEnemy = Vector3.zero;
        }

    }

    public void OnDrawGizmos()
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
    private void ThrowAttack()
    {
        currentWeapon.Throw(this, OnHitVicTim); // Gọi phương thức với tham số
    }
    public void Attack()
    {
        if (currentWeapon != null && currentTime <= 0)
        {

            Invoke(nameof(ThrowAttack), 0.5f);
            currentTime = 3f;
        }
        else
        {
            currentTime -= Time.deltaTime;
        }


    }

    protected virtual void OnHitVicTim(Character accterker, Character Victim)
    {

        if ( Victim !=null && accterker != Victim)
        {
            Victim.Die();
            accterker.UpSize();
        }  

    }
    public void Die()
    {
        ChangeAnim(Constants.DIE_ANIM_NAME);
        Debug.Log("die");
        Destroy(gameObject, 1f);
      
        
    }

    public void UpSize()
    {
        transform.localScale += Vector3.one * 0.5f;
        radius += 1f;
    }

    public void ChangeWeapon(WeaponType weaponType)
    {
        Weapon wp = DataManager.Instance.GetWeapon(weaponType);

        currentWeapon = SimplePool.Spawn<Weapon>(wp, weaponParent);

    }
    //public void ChangeHat(HatType hatType)
    //{

    //}
    //public void ChangePant(PantType pantType)
    //{

    //}
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
