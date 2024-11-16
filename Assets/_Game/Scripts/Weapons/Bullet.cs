using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : GameUnit
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private LayerMask playerLayer;
    protected Character attacker;
    protected Action<Character ,Character >onHit;

    private Vector3 startBullet;
    private float bulletTime;

    private void Start()
    {
        startBullet = transform.position;
        bulletTime = 3f;
    }
    void Update()
    {
        transform.Rotate(new Vector3(0,0, rotationSpeed*Time.deltaTime));
        
        if (bulletTime<=0)
        {
            SimplePool.Despawn(this);
            bulletTime = 3f;
        }
        else
        {
            bulletTime -= Time.deltaTime;
        }
    }
   
    //Set bullet data for bullet
    public virtual void OnInit(Character attacker, Action<Character, Character> onHit)
    {
        this.attacker = attacker;
        this.onHit = onHit;
        
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (((1<<collider.gameObject.layer)&playerLayer)!=0)
        {
            Character victim = Cache.GetCharacter(collider);
            onHit?.Invoke(attacker, victim);
            if (attacker != victim && victim !=null)
            {
                SimplePool.Despawn(this);
            }
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
    //    if (other.CompareTag(Constant.TAG_CHARACTER_NAME))
    //    {
    //        Character victim = Cache.GetCharacter(other);
    //        onHit?.Invoke(attacker, victim);
    //    }
    //}
    //public class WeaponAxe_1 :
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
    //        currentSkin.WeaponAxe_1.Throw(this, OnHitVictim);
    //    }
    //    // Logic when bullet hit victim
    //    protected virtual OnHitVictim(Character attacker, Character victim)
    //    {
    //        victim.DoDead();
    //        .....
    //}

    //}


