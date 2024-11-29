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
    private bool isBullet;

    private void Start()
    {
        bulletTime = 3f;
    }
    void Update()
    {
        if (isBullet)
        {
            return;
        }
        transform.Rotate(new Vector3(0,0, rotationSpeed*Time.deltaTime));
        
        if (bulletTime<=0)
        {
            ResetBullet();
        }
        else
        {
            bulletTime -= Time.deltaTime;
        }
    }

    private void ResetBullet()
    {
        SimplePool.Despawn(this);   
        bulletTime = 3f;
        isBullet = false;
    }

    //Set bullet data for bullet
    public virtual void OnInit(Character attacker, Action<Character, Character> onHit)
    {
        this.attacker = attacker;
        this.onHit = onHit;
        bulletTime = 3f;
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (((1<<collider.gameObject.layer)&playerLayer)!=0)
        {
            
            Character victim = Cache.GetCharacter(collider);
            onHit?.Invoke(attacker, victim);
            if (attacker != victim && victim !=null)
            {
                ResetBullet();
            }
        }
        
    }
}


