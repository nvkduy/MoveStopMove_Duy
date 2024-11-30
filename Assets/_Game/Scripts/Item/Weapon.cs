using System;
using UnityEngine;


public class Weapon : GameUnit
{
    [SerializeField] float attackForce;
    [SerializeField] private Bullet bulletPrefab;
    private Transform setPosBullet;
    private Vector3 transPos;
    public void Throw(Character character, Action<Character, Character> onHit)
    {
        setPosBullet = character.weaponParent;
        transPos = new Vector3(transform.position.x, 0.2f, transform.position.z);
        Vector3 shootDirection = (character.targetEnemy - transPos).normalized;
        Bullet bullet = SimplePool.Spawn<Bullet>(bulletPrefab);
        bullet.TF.position = setPosBullet.position;

        Rigidbody rb = Cache.GetRigidbody(bullet);
        if (rb != null)
        {
            // Reset velocity
            rb.velocity = Vector3.zero;
            rb.AddForce(shootDirection * attackForce);

            gameObject.SetActive(false);
            Invoke(nameof(SetActiveWeapon), 0.5f);
        }

        bullet.OnInit(character, onHit);
    }
    private void SetActiveWeapon()
    {
        gameObject.SetActive(true);
    }



}
