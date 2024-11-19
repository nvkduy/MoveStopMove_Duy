using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Weapon : GameUnit
{
    //private Character character;
    [SerializeField] float attackForce;
    [SerializeField] private Bullet bulletPrefab;
    public void Throw(Character character, Action<Character, Character> onHit)
    {
        Vector3 shootDirection = (character.targetEnemy - transform.position);
        Bullet bullet = SimplePool.Spawn<Bullet>(bulletPrefab);
        // Bullet bullet = LeanPool.Spawn<Bullet>(bulletPrefab);
        if (bullet != null)
        {
            bullet.transform.position = transform.position;
        } 
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            //Đặt lại vận tốc tránh trường hợp sẽ cộng đồn lực khi addforce 
            rb.velocity=Vector3.zero;
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
