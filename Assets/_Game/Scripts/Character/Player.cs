using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private float speedMove = 5f;
    [SerializeField] private FloatingJoystick floatingJoystick;
    [SerializeField] GameObject playerVisual;

    private Vector3 targetPosition;

    private void Start()
    {
        targetPosition = transform.position;
    }
    private void Update()
    {
       

        if (floatingJoystick.Horizontal != 0 && floatingJoystick.Vertical != 0)
        {

            MovePlayer();
        }
        else
        {
            ChangeAnim(Constants.IDLE_ANIM_NAME);
        }

    }

    private void MovePlayer()
    {
        Vector3 direction = Vector3.forward * floatingJoystick.Vertical + Vector3.right * floatingJoystick.Horizontal;
        if (direction != Vector3.zero)
        {
            ChangeAnim(Constants.RUN_ANIM_NAME);
            targetPosition = transform.position + direction * speedMove * Time.deltaTime;
            // transform.rotation = Quaternion.LookRotation(direction);
            Vector3 lookDirection = direction + playerVisual.transform.position;
            playerVisual.transform.LookAt(lookDirection);
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speedMove * Time.deltaTime);

    }

}
