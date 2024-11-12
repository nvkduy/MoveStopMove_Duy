using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.AI;

public class Character : GameUnit
{
    public Weapon currentWeapon;
    public int numOfEnemy;

    [SerializeField] protected float radius;
    [SerializeField] protected NavMeshAgent agent;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] private Animator animator;
    [SerializeField] protected Transform weaponParent;
    [SerializeField] Transform weaponPreviewPoint;

    internal Vector3 targetEnemy;
    internal float currentTime = 0;
    
    protected bool isAttack = false;

    Collider[] hitColliders = new Collider[20];
    private string currentAnim;
    private Weapon currentPreviewWeapon;
    private LevelManager levelManager;
    private int numberBots;
    public int Coins { get; set; } = 1000;


    private void Start()
    {
       // numberBots = levelManager.CountOfBot;
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
    
    public void Attack()
    {
        if (currentWeapon != null && currentTime <= 0)
        {
            
            ChangeAnim(Constants.ATTACK_ANIM_NAME);
            currentWeapon.Throw(this, OnHitVicTim); // Gọi phương thức với tham số            
            currentTime = 3f;
      
        }
    }

    protected void ResetAttack()
    {
        isAttack = true;
    }

    protected virtual void OnHitVicTim(Character accterker, Character Victim)
    {

        if ( Victim !=null && accterker != Victim)
        {
            
            Victim.Die();
            accterker.UpSize();
        }
        if (accterker is Player && numberBots ==0)
        {
            
            //UIManager.Instance.OpenUI<CanvasVictory>();
        }
    }
    public void Die()
    {
        Collider collider = GetComponent<Collider>();
        ChangeAnim(Constants.DIE_ANIM_NAME);
        collider.enabled = false;
        Destroy(gameObject, 1f);
        LevelManager.Instance.RemoveBots();
        
    }

    public void UpSize()
    {
        
        transform.localScale += Vector3.one * 0.5f;
        radius += 1f;
    }

    public void ChangeWeapon(WeaponType weaponType)
    {
        Weapon wp = DataManager.Instance.GetWeapon(weaponType);
        if (currentPreviewWeapon != null)
        {
            SimplePool.Despawn(currentPreviewWeapon);
            currentPreviewWeapon = null;
        }

        currentWeapon = SimplePool.Spawn<Weapon>(wp, weaponParent);

    }
    public void PreviewWeapon(WeaponType weaponType)
    {
        Weapon weaponPrefab = DataManager.Instance.GetWeapon(weaponType);
        if (currentPreviewWeapon != null)
        {
            SimplePool.Despawn(currentPreviewWeapon);
        }
        currentPreviewWeapon = SimplePool.Spawn<Weapon>(weaponPrefab, weaponPreviewPoint);
        currentPreviewWeapon.transform.localScale = weaponPrefab.transform.localScale;
        currentPreviewWeapon.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        currentPreviewWeapon.transform.SetParent(weaponPreviewPoint);

        Debug.Log("PreviewWeapon: " + weaponPrefab.name);
    }
    //public void ChangeHat(HatType hatType)
    //{

    //}
    //public void ChangePant(PantType pantType)
    //{

    //}

    //public void PreviewSkin(SkinType skinType)
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
