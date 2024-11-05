using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private float speedMove = 5f;
    [SerializeField] private FloatingJoystick floatingJoystick;
    [SerializeField] GameObject playerVisual;
   
    private Vector3 targetPosition;

    float horizontal;
    float vertical;
    

    private void Update()
    {
        if (GetInPut())
        {
            MovePlayer();
        }
        else if (targetEnemy != Vector3.zero && !isAttack)
        {
            ChangeAnim(Constants.ATTACK_ANIM_NAME);
            Attack();
        }
        else if(!GetInPut() && !isAttack)
        {
             
            ChangeAnim(Constants.IDLE_ANIM_NAME);       
        }
        FindEnemy(transform.position, radius);
    }

    public override void OnInit()
    {

        base.OnInit();
        ChangeWeapon(WeaponType.Axe1);
    }

    private void OnEnable()
    {
        OnInit();
    }

    private bool GetInPut()
    {
        horizontal = floatingJoystick.Horizontal;
        vertical = floatingJoystick.Vertical;
        if (Mathf.Abs(horizontal) < 0.01f && Mathf.Abs(vertical) < 0.01f)
        {
            return false;
        }
        return true;
    }
    private void MovePlayer()
    {
        Vector3 direction = Vector3.forward * floatingJoystick.Vertical + Vector3.right * floatingJoystick.Horizontal;
        if (direction != Vector3.zero)
        {
            ChangeAnim(Constants.RUN_ANIM_NAME);
            targetPosition = transform.position + direction * speedMove * Time.deltaTime;
            Vector3 lookDirection = direction + playerVisual.transform.position;
            playerVisual.transform.LookAt(lookDirection);
        }
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speedMove * Time.deltaTime);
    }
}
