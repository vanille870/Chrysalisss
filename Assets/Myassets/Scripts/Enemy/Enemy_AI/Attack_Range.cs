using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Range : MonoBehaviour
{
    public Animator slimeAnimator;
    public Enemy_Animation EnemyAnimationScript;
    public int triggerNumber;

    void Start()
    {

    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            EnemyAnimationScript.TriggerAttack(triggerNumber);
        }
    }




}
