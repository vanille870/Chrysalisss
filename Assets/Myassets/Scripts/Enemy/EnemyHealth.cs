using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyHealth : MonoBehaviour
{
    public int StaggerValue;
    public int defence;
    int maxStaggerValue;
    int lastAttackHitNumber;

    public int EnemyCurrentHealth;
    public Animator EnemyAnimator;

    public Slime_Damaged enemyAnimationScript;
    public Basic_Enemy_AI AIScript;
    [SerializeField] ParticleSystem enemyHitEffect;
    public bool EnemyIsDead;


    // Start is called before the first frame update
    void Start()
    {
        maxStaggerValue = StaggerValue;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void EnemyRecieveDamage(int recievedDamage, int staggerDamage, int attackNumber)
    {
        if (lastAttackHitNumber != attackNumber)
        {
            EnemyCurrentHealth -= recievedDamage;
            StaggerValue -= staggerDamage;
            AIScript.seenPlayer = true;
            AIScript.currentEnemyState = Basic_Enemy_AI.EnemyState.chasing;
            enemyHitEffect.Play();

            if (EnemyCurrentHealth <= 0)
            {
                EnemyAnimator.SetTrigger("_Dead");
                enemyAnimationScript.DeadAnimation();
                EnemyIsDead = true;
                return;
            }

            if (StaggerValue <= 0 && EnemyIsDead == false)
            {
                StaggerValue = maxStaggerValue;
                EnemyAnimator.SetTrigger("_Stagger");
                enemyAnimationScript.FlashStart();

                lastAttackHitNumber = attackNumber;
            }

            else
            {
                enemyAnimationScript.FlashStart();

                lastAttackHitNumber = attackNumber;
            }
        }


        if (lastAttackHitNumber == attackNumber)
            print("surplus hit blocked");
    }
}
