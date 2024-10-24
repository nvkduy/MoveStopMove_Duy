using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private string currentAnim;

    public void OnInit()
    {

    }
    public void Attack(Transform position)
    {

    }

    public void ChangeWeapon(WeaponType weaponType)
    {

    }
    public void ChangeHat(HatType hatType)
    {

    }
    public void ChangePant(PantType pantType)
    {

    }
    public void ChangeAnim(string animName)
    {
        if (currentAnim != animName)
        {
            animator.ResetTrigger(animName);
            currentAnim = animName;
            animator.SetTrigger(currentAnim);
        }
    }
}
