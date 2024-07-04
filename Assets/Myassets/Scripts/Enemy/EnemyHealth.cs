using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyHealth : MonoBehaviour
{
    public int StaggerValue;
    public int defence;
    int maxStaggerValue;
    public int lastAttackHitNumber = 0;

    public int EnemyCurrentHealth;
    public Animator EnemyAnimator;

    public Slime_Damaged enemyAnimationScript;
    public Enemy_Sound slimeSoundScript;
    public Basic_Enemy_AI AIScript;
    [SerializeField] ParticleSystem enemyHitEffect;
    public bool EnemyIsDead;

    public Transform trans;


    // Start is called before the first frame update
    void Start()
    {
        maxStaggerValue = StaggerValue;
    }

    public void EnemyRecieveDamage(int recievedDamage, int staggerDamage)
    {
        EnemyCurrentHealth -= recievedDamage;
        StaggerValue -= staggerDamage;
        AIScript.seenPlayer = true;
        AIScript.currentEnemyState = Basic_Enemy_AI.EnemyState.chasing;
        enemyHitEffect.Play();

        if (EnemyCurrentHealth <= 0)
        {
            EnemyDeath();
            return;
        }

        else
            slimeSoundScript.PlayHitSound();

        if (StaggerValue <= 0 && EnemyIsDead == false)
        {
            StaggerValue = maxStaggerValue;
            EnemyAnimator.SetTrigger("_Stagger");
        }

        enemyAnimationScript.FlashStart();
    }



    public void EnemyDeath()
    {
        slimeSoundScript.PlayDeathSlashSound();
        EnemyAnimator.SetTrigger("_Dead");
        enemyAnimationScript.DeadAnimation();
        EnemyIsDead = true;
    }
}

