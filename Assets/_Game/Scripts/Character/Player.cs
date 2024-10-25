using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private float speedMove = 5f;
    [SerializeField] private FloatingJoystick floatingJoystick;
    [SerializeField] GameObject playerVisual;

    private Vector3 targetPosition;
    bool isAttack = false;
    float horizontal;
    float vertical;
   
    private void Update()
    {


        if (GetInPut() == true)
        {
            MovePlayer();
        }
        else if (targetEnemy != Vector3.zero && isAttack == false)
        {       
                ChangeAnim(Constants.ATTACK_ANIM_NAME);
                Attack();
                isAttack = true;
                StartCoroutine(ResetAttack());           
        }
        else
        {
            ChangeAnim(Constants.IDLE_ANIM_NAME);
   
        }
        FindTarget(transform.position, 2);



    }
    public override void OnInit()
    {


    }

    private IEnumerator ResetAttack()
    {
        ChangeAnim(Constants.IDLE_ANIM_NAME);
        yield return new WaitForSeconds(2.5f);
        isAttack = false;
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
