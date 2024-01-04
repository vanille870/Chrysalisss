using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;

public class Enemy_StaggerSM : StateMachineBehaviour
{
    public Enemy_Animation Enemy_AnimationScript;

    // Start is called before the first frame update
    void OnStateEnter()
    {
        Enemy_AnimationScript.slimeAgent.speed = 0;
        Enemy_AnimationScript.slimeAgent.angularSpeed = 0;
        Debug.Log("start stagger");
    }

    void OnStateExit()
    {
        Enemy_AnimationScript.RestoreFromStagger();
        
    }
}
