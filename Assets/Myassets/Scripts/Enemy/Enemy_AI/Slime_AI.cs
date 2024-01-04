using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Slime_AI : MonoBehaviour
{

    static NavMeshAgent slimeAgent;
    public Transform PlayerPoint;
    Animator slimAnimator;
    public float FOVAngle;
    public int EnemyFOV;
    public int SpottingDistance = 50;
    public float DistanceToPlayer = 50;

    public bool IsDead = false;
    public bool playerIsSeen = false;


    // Start is called before the first frame update
    void Start()
    {
        slimeAgent = gameObject.transform.GetComponentInParent<NavMeshAgent>();
        slimAnimator = GetComponentInChildren<Animator>();
        PlayerPoint = GameObject.Find("PlayerPoint").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        DistanceToPlayer = Vector3.Distance(PlayerPoint.position ,transform.position);
        FOVAngle = Vector3.Angle(transform.forward, (PlayerPoint.position - transform.position).normalized);

        if (slimeAgent.enabled && FOVAngle < EnemyFOV)
        {
            //DistanceToPlayer = PlayerPoint.position.sqrMagnitude - transform.position.sqrMagnitude;

            if (DistanceToPlayer < SpottingDistance)
            {
                playerIsSeen = true;
            }
        }

        if (playerIsSeen == true)
        {
            slimeAgent.SetDestination(PlayerPoint.position);
        }

        
        Color active = (FOVAngle < EnemyFOV) ? Color.green : Color.red;

        Debug.DrawRay(transform.position, transform.forward, Color.red);
        Debug.DrawRay(transform.position, (PlayerPoint.position - transform.position).normalized, active);
    }
}
