using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;

public class Normal_attack : StateMachineBehaviour
{
    public event System.Action<Animator, AnimatorStateInfo> onStateEnterOR;
    public GeneralAnimationWeapon generalAnimationWeapon;
    public PlayerMovement movement;


    public void Push()
    {

    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        onStateEnterOR?.Invoke(animator, stateInfo);

        animator.ResetTrigger("_NormalAttack");
        animator.SetBool("CanStartNextAttack", false);
        animator.ResetTrigger("_ReturnToIdle");

        movement.CallAttackPush();

        On_enemy_hit.AttackNumber += 1;
    }

    void OnStateExit()
    {
        On_breakable_hit.canBreakObjects = false;
        generalAnimationWeapon.stopSparkle = true;
    }


    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
