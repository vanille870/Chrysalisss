using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Slime_animation : MonoBehaviour
{
    Animator slimAnimator;
    public float currentSpeed;
    public float speedPercent;


    NavMeshAgent slimeAgent;


    // Start is called before the first frame update
    void Start()
    {
        slimeAgent = GetComponentInParent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
   
        //moveBlend = Mathf.SmoothDamp(moveBlend, moveDir.magnitude, ref currentMBVelocity, AnimAcceleration);
        currentSpeed = slimeAgent.velocity.sqrMagnitude;
        speedPercent = currentSpeed/slimeAgent.speed;

    }
}
