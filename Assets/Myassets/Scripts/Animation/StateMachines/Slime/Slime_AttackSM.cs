using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_AttackSM : StateMachineBehaviour
{
    public Slime_animation SMslime_AnimationScript;

    public void OnStateEnter()
    {
        SMslime_AnimationScript.StartAttack(); 
        
    }

    public void OnStateExit()
    {
        SMslime_AnimationScript.ClearBools(); 
    }
}
