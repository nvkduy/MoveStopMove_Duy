﻿using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] protected float radius;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] Transform weaponParent;
    //[SerializeField] GameObject attackPrefab;
    public bool isAttack = false;
    public Weapon currentWeapon;
    int numOfEnemy;
    Collider[] hitColliders = new Collider[20];

    internal Vector3 targetEnemy;
    private string currentAnim;



    //private void Start()
    //{
    //    OnInit();
    //}
    public virtual void OnInit()
    {
        ChangeWeapon(WeaponType.Axe1);
    }
    public void FindTarget(Vector3 position, float radius)
    {

        numOfEnemy = Physics.OverlapSphereNonAlloc(position, radius, hitColliders, enemyLayer);
        float minDistance = Mathf.Infinity;
        Collider nearestEnemy = null;

        for (int i = 0; i < numOfEnemy; i++)
        {

            float distance = Vector3.Distance(position, hitColliders[i].transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestEnemy = hitColliders[i];
            }
        }
        if (nearestEnemy != null)
        {
            targetEnemy = nearestEnemy.transform.position;

            Debug.Log("Enemy gần nhất: " + nearestEnemy.name);
        }
        if (numOfEnemy == 0)
        {
            targetEnemy = Vector3.zero;
        }

    }
    private void CongDiem()
    {
        Debug.Log(gameObject.name + " da duoc ong diem");
    }
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);

        for (int i = 0; i < numOfEnemy; i++)
        {
            Gizmos.DrawLine(transform.position, hitColliders[i].transform.position);
        }
    }
    public void Attack()
    {
        if (currentWeapon != null)
        {
            currentWeapon.Throw(this, OnHitVicTim);
            Debug.Log("đã attack");

        }


    }
    protected virtual void OnHitVicTim(Character accterker, Character Victim)
    {
        accterker.isAttack = true;  
        Victim.Die();
        accterker.UpSize();
        

    }
    public void Die()
    {
        
        
        ChangeAnim(Constants.DIE_ANIM_NAME);
        Debug.Log("die");

        Destroy(gameObject, 1f);
    }

    public void UpSize()
    {
        Debug.Log("currentRadius" + radius);
        Debug.Log("UpSize() is called"); // Kiểm tra xem hàm có chạy không
        transform.localScale += Vector3.one;
        radius += 1f;
        Debug.Log("LastRadius" + radius);
        Debug.Log("Size: " + transform.localScale);

    }

    public void ChangeWeapon(WeaponType weaponType)
    {
        Weapon wp = DataManager.Instance.GetWeapon(weaponType);

        currentWeapon = SimplePool.Spawn<Weapon>(wp, weaponParent);

        //Instantiate(wp.gameObject, weaponParent.position, Quaternion.identity, weaponParent);
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
