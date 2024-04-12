using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_IdleSM : StateMachineBehaviour
{

    public Enemy_Animation Enemy_AnimationScript;

    public void OnStateEnter()
    {
        Enemy_AnimationScript.FinishAttack();
        Enemy_AnimationScript.RestoreFromStagger();
    }

    public void OnStateExit()
    {
        
    }
}
