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
    [SerializeField] bool hasPerformedCaThisPress = false;

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

    [SerializeField]
    TimedEvent chargeTimer = new TimedEvent();
    [SerializeField]
    TimedEvent forceChargeTimer = new TimedEvent();
    [SerializeField]
    TimedEvent waitAfterNormalAttack = new TimedEvent();

    public void StopSparkle()
    {
        stopSparkle = true;
    }

    // Update is called once per frame
    void Update()
    {
        isAttacking_DEBUG = isAttacking;
        ResetSpeedVariable();
        debugg = forceChargeTimer.IsFinished;

        if (stopSparkle == false && chargeTimer.IsFinished == true)
        {
            sparklAnimator.SetTrigger("_Sparkle");
            stopSparkle = true;

        }

        if (performedChargeAttack == false && forceChargeTimer.IsFinished == true)
        {
            mainCharAnimator.SetTrigger("_ForceChargeAttack");
            mainCharAnimator.SetBool("_ChargeAttackHeld", false);
            performedChargeAttack = true;
            hasPerformedCaThisPress = true;
        }
    }

    public void StartNormalAttacks()
    {
        mainCharAnimator.SetTrigger("_NormalAttack");
        isAttacking = true;
        waitAfterNormalAttack.SetClock();
    }

    public void ResetSpeedVariable()
    {
        if (isRessetingSpeed == true)
        {
            if (waitAfterNormalAttack.IsFinished)
            {
                isAttacking = false;
                isRessetingSpeed = false;
                mainCharAnimator.SetTrigger("_ReturnToIdle");
            }
        }

    }

    //called from input
    public void SetChargeAttackInAnimator()
    {
        if (hasPerformedCaThisPress == false)
        {
            mainCharAnimator.SetBool("_ChargeAttackHeld", true);
            hasPerformedCaThisPress = true;
        }
    }

    //called from stateMachine
    public void StartChargeAttack()
    {
        chargeTimer.SetClock();
        forceChargeTimer.SetClock();
        movement.StartStuckInAttack();
        stopSparkle = false;
        performedChargeAttack = false;
    }

    public void PerformChargeAttack()
    {
        mainCharAnimator.SetBool("_ChargeAttackHeld", false);
        stopSparkle = true;
        hasPerformedCaThisPress = false;
        performedChargeAttack = true;

        if (chargeTimer.IsFinished)
        {
            mainCharAnimator.SetFloat("_ChargeAttackSpeed", ChargedChargeSpeed);
            ChargeAttackCharged = true;
        }

        else
        {
            mainCharAnimator.SetFloat("_ChargeAttackSpeed", NormalChargeSpeed);
            ChargeAttackCharged = false;
        }

    }
}
