using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private float radius;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] GameObject attackPrefab;
    [SerializeField] Transform attackParent;
    [SerializeField] float attackForce;
    int numOfEnemy;
    Collider[] hitColliders = new Collider[20];
    private string currentAnim;
    Vector3 targetEnemy;


    public virtual void OnInit()
    {

    }
    public void FindTarget(Vector3 position, float radius)
    {
      
        numOfEnemy = Physics.OverlapSphereNonAlloc(position, radius,hitColliders,enemyLayer);
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
            
            Debug.Log("Enemy gần nhất: " + nearestEnemy.name);
        }
        //targetEnemy = nearestEnemy.transform.position;
        


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
        //Vector3 shootDirection
        GameObject bullet = Instantiate(attackPrefab, transform.position, Quaternion.identity, attackParent);
        bullet.GetComponent<Rigidbody>().AddForce(targetEnemy * attackForce);
    }

    public void ChangeWeapon(WeaponType weaponType)
    {

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
