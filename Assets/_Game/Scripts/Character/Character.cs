using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] protected float radius;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] GameObject attackPrefab;
    [SerializeField] Transform attackParent;
    [SerializeField] float attackForce;
    [SerializeField] float rotationSpeed;
    [SerializeField] private GameObject bulletVisual;
    int numOfEnemy;
    Collider[] hitColliders = new Collider[20];
    private string currentAnim;
    protected Vector3 targetEnemy;
    public UnityAction onDie;



    public virtual void OnInit()
    {

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
            nearestEnemy.gameObject.GetComponent<Character>().onDie += CongDiem;
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

        Vector3 shootDirection = (targetEnemy - transform.position).normalized;
        GameObject bullet = Instantiate(attackPrefab, transform.position, Quaternion.identity, attackParent);
        bullet.GetComponent<Rigidbody>().AddForce(shootDirection * attackForce);
        bullet.transform.position = transform.position + Vector3.up * 0.5f;
        bulletVisual.transform.rotation = Quaternion.Euler(-90, 180, 0);


    }
    public void Die()
    {
        onDie?.Invoke();
    }

    public void UpSize()
    {
        transform.localScale += Vector3.one;
        radius += 1f;
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
