using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyHealth : MonoBehaviour
{
    public int StaggerValue;
    int maxStaggerValue;

    public int EnemyCurrentHealth;
    public Animator EnemyAnimator;

    public Slime_Damaged enemyAnimationScript;
    public Slime_AI slime_AIScript;
    public bool EnemyIsDead;

    public float time;


    // Start is called before the first frame update
    void Start()
    {
        maxStaggerValue = StaggerValue;
    }

    // Update is called once per frame
    void Update()
    {
        time = Time.time;
    }

    public void EnemyRecieveDamage(int recievedDamage, int staggerDamage)
    {
        EnemyCurrentHealth -= recievedDamage;
        StaggerValue -= staggerDamage;
        slime_AIScript.currentEnemyState = Slime_AI.EnemyState.chasing;


          if (EnemyCurrentHealth <= 0)
        {
            EnemyAnimator.SetTrigger("_Dead");
            enemyAnimationScript.DeadAnimation();
            EnemyIsDead = true;
        }

        if (StaggerValue <= 0 && EnemyIsDead == false)
        {
            StaggerValue = maxStaggerValue;
            EnemyAnimator.SetTrigger("_Stagger");

            print("staggered");
        }
    }
}
