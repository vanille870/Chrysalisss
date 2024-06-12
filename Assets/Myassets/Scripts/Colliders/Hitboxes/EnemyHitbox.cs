using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitbox : MonoBehaviour
{
    public Transform playerPoint;
    public PlayerMovement movementScript;
    public PlayerConditionStats playerConditionStatsScript;
    public MainCharAnimation mainCharAnimationScript;
    CharacterController characterControllerPlayer;


    public float KnockBackAmount;
    public int attackDamage;

    public GameObject StaggerpointGO;
    Vector3 EnemyStaggerPointPos;


    // Start is called before the first frame update
    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            movementScript.StartKnockBack(KnockBackAmount, StaggerpointGO.transform.position);
            playerConditionStatsScript.RecieveDamage(attackDamage);
            CheckIfHittingBack();
        }
    }

    void CheckIfHittingBack()
    {
        Vector3 toTarget = (transform.position - playerPoint.transform.position).normalized;

        if (Vector3.Dot(playerPoint.transform.forward, toTarget) > 0)
        {
            mainCharAnimationScript.Stagger();
            print("front");
        }
        else
        {
            mainCharAnimationScript.StaggerBack();
            print("Back");
        }
    }
}
