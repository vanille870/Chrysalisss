using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Slime_animation : MonoBehaviour
{
    public NavMeshAgent slimeAgent;
    Animator slimeAnimator;
    public GameObject TriggerCollectionGO;

    public float currentSpeed;
    [Range(0, 1)]
    public float speedPercent;

    public float attackspeed;
    public float attackAcceleration;
    public float attackAnimationSmoothFactor;

    float agentOriginalSpeed;
    float agentOrginalAcceleration;


    SphereCollider attackRangeSphere;
    public Transform PlayerPoint;
    bool SmoothBlendWhenAttacking;
    bool isRoatingToPlayer;
    public bool isAttacking;
    public float rotationSpeed;

    public Collider[] TriggerCollection;
    public int[] TriggerIDs;
    public int hdzéua;







    // Start is called before the first frame update
    void Start()
    {
        TriggerCollection = TriggerCollectionGO.GetComponentsInChildren<Collider>();

        slimeAnimator = GetComponent<Animator>();

        PlayerPoint = GameObject.Find("PlayerPoint").GetComponent<Transform>();

        agentOriginalSpeed = slimeAgent.speed;
        agentOrginalAcceleration = slimeAgent.acceleration;

        foreach (GameObject GO in TriggerCollectionGO.transform)
        {
            if (GO.CompareTag("Enemy_trigger"))
            {
                int number = 0;

                string GOname = GO.name;
                TriggerIDs[number] = Animator.StringToHash(GOname);

                number += 1;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {

        currentSpeed = slimeAgent.velocity.magnitude;
        speedPercent = currentSpeed / slimeAgent.speed;
        slimeAnimator.SetFloat("_IdleBlend", speedPercent);

        /*if (SmoothBlendWhenAttacking == false)
        {
            currentSpeed = slimeAgent.velocity.magnitude;
            speedPercent = currentSpeed / slimeAgent.speed;
            slimAnimator.SetFloat("_IdleBlend", speedPercent);
        }

        else
        {
            speedPercent -= Time.deltaTime * attackAnimationSmoothFactor;
            speedPercent = Mathf.Clamp(speedPercent, 0, Mathf.Infinity);
            slimAnimator.SetFloat("_IdleBlend", speedPercent);
        }*/
    }

    public void StartAttack()
    {
        if (isAttacking == false)
        {
            SmoothBlendWhenAttacking = true;
            slimeAgent.speed = attackspeed;
            slimeAgent.acceleration = attackAcceleration;
            isAttacking = true;
        }

    }

    public void FinishAttack()
    {
        SmoothBlendWhenAttacking = false;
        isAttacking = false;
        slimeAgent.speed = agentOriginalSpeed;
        slimeAgent.acceleration = agentOrginalAcceleration;

        slimeAnimator.SetBool("_FirstAttack", false);
        slimeAnimator.SetBool("_InTrigger0", false);
        slimeAnimator.SetBool("_InTrigger1", false);
    }

    public void ClearBools()
    {
        slimeAnimator.SetBool("_FirstAttack", false);
        slimeAnimator.SetBool("_InTrigger0", false);
        slimeAnimator.SetBool("_InTrigger1", false);
    }

    public void ContinueAttack()
    {

    }

    public void CheckIfPlayerIsInRange(int colliderNumber)
    {
        
    }

    public void CheckIfPlayerIsInRange2(String colliderName)
    {
    
    }

    public void SetTriggerBool(int colliderNumber)
    {
        switch (colliderNumber)
        {
            case 0:

                slimeAnimator.SetBool("_InTrigger0", true);
                break;

            case 1:

                slimeAnimator.SetBool("_InTrigger1", true);
                break;
        }

    }


    //key events
    public void CheckIfPlayerIsInRangeKEY()
    {
        /*if (attackRangeSphere.bounds.Contains(PlayerPoint.position))
        {
            StartAttack();
        }

        else
        {
            FinishAttack();
        }*/
    }

    void RotateTowardsTarget()
    {
        // Determine which direction to rotate towards
        Vector3 targetDirection = PlayerPoint.position - transform.position;

        float singleStep = rotationSpeed * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

        // Draw a ray pointing at our target in
        Debug.DrawRay(transform.position, newDirection, Color.red);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }
}
