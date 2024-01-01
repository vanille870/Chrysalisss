using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_IdleSM : StateMachineBehaviour
{

    public Slime_animation SMslime_AnimationScript;

    public void OnStateEnter()
    {
        SMslime_AnimationScript.CheckIfPlayerIsInRangeKEY();
        SMslime_AnimationScript.FinishAttack();
    }

    public void OnStateExit()
    {
        
    }
}
