using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private Character character;
    [SerializeField] float attackForce;
    [SerializeField] private Bullet bulletPrefab;
    public void Shoot(Character character, Action<Character , Character > onHit)
    {
        Vector3 shootDirection = (character.targetEnemy - transform.position);
        Bullet bullet = SimplePool.Spawn<Bullet>(PoolType.Bullet_1);
        bullet.TF.position= transform.position;
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null) 
        {
            rb.AddForce(shootDirection*attackForce);
        }
        bullet.OnInit(character, onHit);
    }

   


}
