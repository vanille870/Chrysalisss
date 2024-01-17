using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;

public class Idle_SM : StateMachineBehaviour
{       
  
    public GeneralAnimationWeapon generalAnimationWeapon;
    public PlayerMovement SMmovement;

    public void Awake() 
    {
    
    }

    public void OnStateExit()
    {
        GeneralAnimationWeapon.isAttacking = true; 
    }

    public void OnStateEnter(Animator animator)
    {
        GeneralAnimationWeapon.isAttacking = false;

        animator.SetBool("NormalAttack", false);
        animator.SetBool("CanStartNextAttack", false);
        animator.SetBool("ReturnToIdle", false);
        SMmovement.FinishAttacking();
        SMmovement.StartMoving();
        SMmovement.RestoreSpeedAndTurning();

        
    }
}
