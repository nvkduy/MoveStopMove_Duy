using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private float speedMove = 5f;
    [SerializeField] GameObject playerVisual;

    Vector3 targetPosition;
    float horizontal;
    float vertical;

    private void FixedUpdate()
    {
        if (targetEnemy != Vector3.zero &&!GetInPut())
        {
            Attack();
        }
        currentTime -= Time.deltaTime;
        FindEnemy(transform.position,radius);
        
    }
    private void Update()
    {
        if (GetInPut())
        {
            
            MovePlayer();
        }
        else
        {

            ChangeAnim(Constants.IDLE_ANIM_NAME);
        }
       
    }

    public  void OnInit()
    {
       int currenIndexWeapon =  PlayerPrefs.GetInt("currrenWeapon");
       ChangeWeapon((WeaponType)currenIndexWeapon);
       

    }
    private void SetJoystick(Joystick joystick)
    {
        
    }
    private bool GetInPut()
    {
        if (UIManager.Instance.Joystick != null)
        {
            horizontal = UIManager.Instance.Joystick.Horizontal;
            vertical = UIManager.Instance.Joystick.Vertical;
            if (Mathf.Abs(horizontal) < 0.01f && Mathf.Abs(vertical) < 0.01f)
            {
                return false;
            }
            return true;
        }
        return false;
      
       
    }
    private void MovePlayer()
    {
        Vector3 direction = Vector3.forward * UIManager.Instance.Joystick.Vertical + Vector3.right * UIManager.Instance.Joystick.Horizontal;
        if (direction != Vector3.zero)
        {
            ChangeAnim(Constants.RUN_ANIM_NAME);
            targetPosition = transform.position + direction * speedMove * Time.deltaTime;
            Vector3 lookDirection = direction + playerVisual.transform.position;
            playerVisual.transform.LookAt(lookDirection);
        }
        else
        {
            ChangeAnim(Constants.IDLE_ANIM_NAME);
        }
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speedMove * Time.deltaTime);
    }

    public override void Die()
    {
        base.Die();
        UIManager.Instance.OpenUI<CanvasFail>();
    }
}
