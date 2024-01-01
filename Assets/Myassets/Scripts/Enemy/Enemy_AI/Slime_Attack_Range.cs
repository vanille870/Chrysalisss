using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_Attack_Range : MonoBehaviour
{
    public Animator slimeAnimator;
    public Slime_animation slimeAnimationScript;
    public int triggerNumber;

    void Start()
    {

    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            slimeAnimationScript.SetTriggerBool(triggerNumber);
        }
    }




}
