using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Slime_AI : MonoBehaviour
{

    static NavMeshAgent slimeAgent;
    public Transform waypoint1;


    // Start is called before the first frame update
    void Start()
    {
        slimeAgent = gameObject.transform.GetComponentInParent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        slimeAgent.SetDestination(waypoint1.position);

    }
}
