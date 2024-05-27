using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralAnimationWeapon : MonoBehaviour
{
    public Animator mainCharAnimator;
    [SerializeField] Animator sparklAnimator;
    public PlayerMovement movement;
    public float speedStorage;
    public bool isAttacking;
    public bool isPerformingChargAttack;
    public bool ChargeAttackCharged;
    public bool isAttacking_DEBUG;
    public bool isRessetingSpeed = false;
    public bool returnToIdle = false;
    [HideInInspector]
    public bool stopSparkle;
    bool performedChargeAttack = true;

    public float speedTimer { get; private set; } = 0;
    public float AttackCooldown;
    public float NormalChargeSpeed;
    public float ChargedChargeSpeed;

    [SerializeField] bool debugg;

    [System.Serializable]
    public struct TimedEvent
    {
        [SerializeField]
        [Range(0.0f, 4.0f)]
        private float Duration;
        private float Clock;

        public TimedEvent(float duration, float time = 0f)
        {
            Duration = duration;
            Clock = time;
        }

        public void SetClock()
        {
            Clock = Time.time + Duration;
        }

        public bool IsFinished => Time.time >= Clock;
    }


    public void StopSparkle()
    {
        stopSparkle = true;
    }

    void SparkleUpdate()
    {

        sparklAnimator.SetTrigger("_Sparkle");
        stopSparkle = true;


    }

    public void StartNormalAttacks()
    {
        mainCharAnimator.SetTrigger("_NormalAttack");
        isAttacking = true;
    }


    //called from input
    public void SetChargeAttackInAnimator()
    {
        mainCharAnimator.SetBool("_ChargeAttackHeld", true);
    }

    //called from stateMachine
    public void StartChargeAttack()
    {
        movement.StartStuckInAttack();
        stopSparkle = false;
        performedChargeAttack = false;
    }

    //called from State Machine
    public void DoChargeAttack()
    {
        mainCharAnimator.SetTrigger("_ForceChargeAttack");
        mainCharAnimator.SetBool("_ChargeAttackHeld", false);
        performedChargeAttack = true;
    }

    //called from input
    public void PerformChargeAttack()
    {
        mainCharAnimator.SetBool("_ChargeAttackHeld", false);
        stopSparkle = true;
        performedChargeAttack = true;

        if (ChargeAttackCharged == true)
        {
            DoChargeAttack();
            mainCharAnimator.SetFloat("_ChargeAttackSpeed", ChargedChargeSpeed);
        }

        else
        {
            DoChargeAttack();

            mainCharAnimator.SetFloat("_ChargeAttackSpeed", NormalChargeSpeed);
        }

        ChargeAttackCharged = false;

    }

}
