using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Weapon : GameUnit
{
    [SerializeField] float attackForce;
    [SerializeField] private Bullet bulletPrefab;
     private Transform bulletParent;
    public void Throw(Character character, Action<Character, Character> onHit)
    {
        bulletParent = character.weaponParent;
        Vector3 shootDirection = (character.targetEnemy - transform.position).normalized;
        Bullet bullet = SimplePool.Spawn<Bullet>(bulletPrefab);
        bullet.TF.position = bulletParent.position;
        shootDirection.y = 0.2f;
        
        Debug.Log("character.weaponParent: " + character.weaponParent);
        
        Rigidbody rb = Cache.GetRigidbody(bullet);
        if (rb != null)
        {
            //Đặt lại vận tốc tránh trường hợp sẽ cộng đồn lực khi addforce 
            rb.velocity=Vector3.zero;
            rb.AddForce(shootDirection* attackForce);

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
