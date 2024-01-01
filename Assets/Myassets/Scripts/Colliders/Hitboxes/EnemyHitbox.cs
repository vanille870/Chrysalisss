using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitbox : MonoBehaviour
{
    public PlayerMovement movementScript;
    public PlayerConditionStats playerConditionStatsScript;
    Vector3 positionEnemy;

    public float KnockBackAmount;
    public int attackDamage;

    GameObject player;

    void Start()
    {

    }


    void GetPositionOfEnemy()
    {
        positionEnemy = transform.root.transform.position;
    }

    // Start is called before the first frame update
    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            GetPositionOfEnemy();
            movementScript.iniateKnockback(KnockBackAmount, positionEnemy);
            playerConditionStatsScript.RecieveDamage(attackDamage);
        }
    }
}
