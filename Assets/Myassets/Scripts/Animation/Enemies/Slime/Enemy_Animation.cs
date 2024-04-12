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
    [Range(0, 1)]
    public float AIMagnitudeTest;

    public float attackspeed;
    public float attackAcceleration;
    public float attackAnimationSmoothFactor;
    [SerializeField] float SmoothTimeFromAttacking;
    [SerializeField] float SmoothTimeFromStagger;

    float agentOriginalSpeed;
    float agentOrginalAcceleration;
    float agentOrginalAnglSpeed;


    SphereCollider attackRangeSphere;
    public Transform PlayerPoint;
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

    void Update()
    {
        AIMagnitudeTest = slimeAgent.velocity.magnitude;

        currentSpeed = slimeAgent.velocity.magnitude;
        speedPercent = currentSpeed / 0.7f;
        slimeAnimator.SetFloat("_IdleBlend", speedPercent);
    }

    public void StartAttack()
    {
        if (isAttacking == false)
        {

            slimeAgent.speed = attackspeed;
            slimeAgent.acceleration = attackAcceleration;
            isAttacking = true;
        }

    }

    public void FinishAttack()
    {
        isAttacking = false;

        slimeAgent.speed = agentOriginalSpeed;
        slimeAgent.acceleration = agentOrginalAcceleration;
    }

    public void StartStagger()
    {
        slimeAgent.speed = 0;
        slimeAgent.angularSpeed = 0;
        slimeAgent.acceleration = 10;
    }

    public void RestoreFromStagger()
    {
        slimeAgent.speed = agentOriginalSpeed;
        slimeAgent.angularSpeed = agentOrginalAnglSpeed;
        slimeAgent.acceleration = agentOrginalAcceleration;
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
