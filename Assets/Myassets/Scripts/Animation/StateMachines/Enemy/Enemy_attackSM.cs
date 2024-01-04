using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_attackSM : StateMachineBehaviour
{
    public Enemy_Animation SMslime_AnimationScript;

    public void OnStateEnter()
    {
        SMslime_AnimationScript.StartAttack(); 
        
    }

    public void OnStateExit()
    {
        SMslime_AnimationScript.ResetAnimatorINT(); 
    }
}
