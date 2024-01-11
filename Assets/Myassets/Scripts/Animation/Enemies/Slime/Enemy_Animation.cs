using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Animation : MonoBehaviour
{
    public NavMeshAgent slimeAgent;
    Animator slimeAnimator;
    public GameObject TriggerCollectionGO;
    public GameObject CollisonGO;
    public GameObject EnemyHurtBoxGO;
    public ParticleSystem EnemyDeathParticleSystem;

    public float currentSpeed;
    [Range(0, 1)]
    public float speedPercent;

    public float attackspeed;
    public float attackAcceleration;
    public float attackAnimationSmoothFactor;

    float agentOriginalSpeed;
    float agentOrginalAcceleration;
    float agentOrginalAnglSpeed;


    SphereCollider attackRangeSphere;
    public Transform PlayerPoint;
    bool SmoothBlendWhenAttacking;
    bool isRoatingToPlayer;
    public bool isAttacking;
    public float rotationSpeed;

    public Collider[] TriggerCollection;


    // Start is called before the first frame update
    void Start()
    {
        slimeAnimator = GetComponent<Animator>();


        PlayerPoint = GameObject.Find("PlayerPoint").GetComponent<Transform>();

        agentOriginalSpeed = slimeAgent.speed;
        agentOrginalAcceleration = slimeAgent.acceleration;
        agentOrginalAnglSpeed = slimeAgent.angularSpeed;


        for (int i = 0; i < TriggerCollectionGO.transform.childCount; i++)
        {
            GameObject currentGO = TriggerCollectionGO.transform.GetChild(i).gameObject;

            if (currentGO.CompareTag("Enemy_trigger"))
            {
                TriggerCollection[i] = currentGO.GetComponent<Collider>();
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
        print(slimeAgent.acceleration);
        print(slimeAgent.speed);
    }

    public void RestoreFromStagger()
    {
        slimeAgent.speed = agentOriginalSpeed;
        slimeAgent.angularSpeed = agentOrginalAnglSpeed;
    }

    public void ResetAnimatorINT()
    {
        slimeAnimator.SetInteger("_TriggerINT", -1);

    }

    public void CheckIfPlayerIsInRange(int colliderNumber)
    {
        if (TriggerCollection[colliderNumber].bounds.Contains(PlayerPoint.position) == true)
        {
            slimeAnimator.SetInteger("_TriggerINT", colliderNumber);
        }
    }

    public void SetTriggerBool(int colliderNumber)
    {
        slimeAnimator.SetInteger("_TriggerINT", colliderNumber);
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

    public void DeactivateEnemy()
    {
        gameObject.transform.root.gameObject.SetActive(false);
    }

    public void TriggerDeathParticleSystemAndMakeEnemyUninteractive()
    {
        EnemyDeathParticleSystem.Play();
        CollisonGO.SetActive(false);

    }

    public void DisableAgent()
    {
        slimeAgent.enabled = false;
    }
}
