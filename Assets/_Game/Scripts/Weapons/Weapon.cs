using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : GameUnit
{
    private Character character;
    [SerializeField] float attackForce;
    [SerializeField] private Bullet bulletPrefab;
    public void Shoot(Character character, Action<Character , Character > onHit)
    {
        Vector3 shootDirection = (character.targetEnemy - transform.position);
        Bullet bullet = SimplePool.Spawn<Bullet>(bulletPrefab);
        bullet.TF.position= transform.position;
        Debug.Log("da ban");
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null) 
        {
            rb.AddForce(shootDirection*50f);
            Debug.Log("da addforce");
        }
        bullet.OnInit(character, onHit);
    }

   


}
