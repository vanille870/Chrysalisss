using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Normal_attack : StateMachineBehaviour
{
    public event System.Action<Animator, AnimatorStateInfo> onStateEnterOR;
    public GeneralAnimationWeapon generalAnimationWeapon;
    public Movement movement;

  
    public void Push()
    {
    
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        onStateEnterOR?.Invoke(animator, stateInfo);

        animator.SetBool("NormalAttack", false);
        animator.SetBool("CanStartNextAttack", false);
        animator.SetBool("ReturnToIdle", false);

        movement.PushOnAttack();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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
