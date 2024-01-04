using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_SM_variables : MonoBehaviour
{
    private Animator EnemyAnimator;
    Enemy_Animation Enemy_AnimationScript;

    void Start()
    {
        Enemy_AnimationScript = GetComponent<Enemy_Animation>();
        EnemyAnimator = GetComponent<Animator>();
        EnemyAnimator.GetBehaviour<Enemy_IdleSM>().Enemy_AnimationScript = Enemy_AnimationScript;
        EnemyAnimator.GetBehaviour<Enemy_StaggerSM>().Enemy_AnimationScript = Enemy_AnimationScript;

        Enemy_attackSM[] slime_AttackSMs = this.EnemyAnimator.GetBehaviours<Enemy_attackSM>();
        
        foreach (Enemy_attackSM slime_AttackSM in slime_AttackSMs)
        {
            slime_AttackSM.SMslime_AnimationScript = Enemy_AnimationScript;

        }
    }
}
