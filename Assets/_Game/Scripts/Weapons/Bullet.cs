using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] GameObject bulletVisual;
    private float maxDistance = 2.1f;
    private Vector3 startBullet;

    private void Start()
    {
        Character character = Cache.GetCharacter(gameObject);
        startBullet = transform.position;
    }
    void Update()
    {
        bulletVisual.transform.Rotate(new Vector3(0, 0, rotationSpeed));
        float distanceTravelled = Vector3.Distance(startBullet, transform.position);
        if (distanceTravelled > maxDistance)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
            other.gameObject.GetComponent<Character>().Die();
        }
    }
}


    //public class Bullet
    //protected Character attacker;
    //protected Action<Character attacker, Character victim> onHit;
    //// set bullet data for bullet
    //public virtual void OnInit(Character attacker, Action<Character attacker, Character victim> onHit)
    //{
    //    this.attacker = attacker;
    //    this.onHit = onHit;
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag(Constant.TAG_CHARACTER))
    //    {
    //        Character victim = Cache.GetCharacter(other);
    //        onHit?.Invoke(attacker, victim);
    //    }
    //}
    //public class Weapon :
    //{

    //    public void Throw(Character character, Action<Character attacker, Character victim> onHit)
    //    {
    //        Bullet bullet = LeanPool.Spawn(bullet);
    //        bullet.OnInit(character, onHit);
    //    }

    //}
    //Đại Hoàng
    //public class Character :
    //{
    //    public void Throw()
    //    {
    //        currentSkin.Weapon.Throw(this, OnHitVictim);
    //    }
    //    // Logic when bullet hit victim
    //    protected virtual OnHitVictim(Character attacker, Character victim)
    //    {
    //        victim.DoDead();
    //        .....
    //}

    //}


