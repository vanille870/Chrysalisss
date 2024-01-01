using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_SM_variables : MonoBehaviour
{
    private Animator slimeAnimator;
    Slime_animation slime_AnimationScript;

    void Start()
    {
        slime_AnimationScript = GetComponent<Slime_animation>();
        slimeAnimator = GetComponent<Animator>();
        slimeAnimator.GetBehaviour<Slime_IdleSM>().SMslime_AnimationScript = slime_AnimationScript;

        Slime_AttackSM[] slime_AttackSMs = this.slimeAnimator.GetBehaviours<Slime_AttackSM>();
        
        foreach (Slime_AttackSM slime_AttackSM in slime_AttackSMs)
        {
            slime_AttackSM.SMslime_AnimationScript = slime_AnimationScript;

        }
    }
}
