using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : GameUnit
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] GameObject bulletVisual;
    protected Character attacker;
    protected Action<Character ,Character >onHit;
    private float maxDistance = 2.1f;
    private Vector3 startBullet;
    

    private void Start()
    {
        startBullet = transform.position;
        
    }
    void Update()
    {
        
        bulletVisual.transform.Rotate(new Vector3(0,0, rotationSpeed*Time.deltaTime));
        float distanceTravelled = Vector3.Distance(startBullet, transform.position);
        if (distanceTravelled > maxDistance)
        {
            SimplePool.Despawn(this);
            startBullet = transform.position;
            Debug.Log("da despawn");
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
        if (collider.CompareTag(Constants.TAG_CHARACTER_NAME))
        {
            
            Character victim = Cache.GetBot(collider); 
            victim = Cache.GetCharacter(collider);
           
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


