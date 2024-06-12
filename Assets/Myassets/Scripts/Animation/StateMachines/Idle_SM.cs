using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;

public class Idle_SM : StateMachineBehaviour
{

    public GeneralAnimationWeapon generalAnimationWeapon;
    public PlayerMovement SMmovement;
    public GameObject PlayerParentGO;

    private Quaternion playerRot;

    public void Awake()
    {

    }

    public void OnStateExit(Animator animator)
    {
        generalAnimationWeapon.isAttacking = true;
        animator.ResetTrigger("_ForceChargeAttack");
    }

    public void OnStateEnter(Animator animator)
    {
        generalAnimationWeapon.isAttacking = false;

        animator.ResetTrigger("_NormalAttack");
        animator.SetBool("CanStartNextAttack", false);
        animator.ResetTrigger("_ReturnToIdle");
        animator.ResetTrigger("_ForceChargeAttack");
        animator.ResetTrigger("_ToggleFallInPit");
        SMmovement.FinishAttacking();
        SMmovement.StartMoving();
        SMmovement.RestoreSpeedAndTurning();
        generalAnimationWeapon.DoChargeAttack();
        generalAnimationWeapon.isPerformingChargAttack = false;
    }
}
