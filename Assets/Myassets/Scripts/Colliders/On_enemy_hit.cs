using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class On_enemy_hit : MonoBehaviour
{
    public PlayerStats playerStats;
    public GeneralAnimationWeapon generalAnimationWeaponScript;
    public CameraShake cameraShakeScript;

    public static int AttackNumber = 0;
    [SerializeField] int AttackNumberDEBUG;


    public ParticleSystem enemyHiteffect;
    private GameObject enemyGameObject;

    EnemyHealth currentEnemyHealthScript;

    public int damage;
    public int staggerDamage;
    int clampedDamage;
    int clampedStaggerDamage;
    public int chargeDamagePenaltyMultiplier;
    [SerializeField] int chargeDamageMultiplier;
    [SerializeField] float DeathHitStopMultiplier;

    float TimeScaleToUse;
    float OriginalHitStop;

    bool HasAlreadyTriggeredShakeSafetyCheck = false;

    [System.Serializable]
    public struct HitStopEvent
    {
        [HideInInspector]
        public float Duration;
        private float Clock;

        public HitStopEvent(float duration, float time = 0f)
        {
            Duration = duration;
            Clock = time;
        }

        public void SetClock()
        {
            Clock = Time.unscaledTime + Duration;
        }

        public bool IsFinished => Time.unscaledTime >= Clock;
    }

    [System.Serializable]
    public struct TimeScaleAmountsPerAttack
    {
        public float RegularAttack;
        public float ChargedAttack;
        public float UnchargedAttack;
    }

    [System.Serializable]
    public struct DurationPerAttack
    {
        public float ChargedAttack;
        public float RegularAttack;
    }

    [System.Serializable]
    public struct EnemyDeathParams
    {
        public float Duration;
        public float TimeScale;
    }

    [System.Serializable]
    public struct HitStopVariables
    {
        public TimeScaleAmountsPerAttack timeScaleAmountsPerAttack;
        public DurationPerAttack durationPerAttack;
        public EnemyDeathParams enemyDeathParams;
    }

    [SerializeField]
    private HitStopEvent HitStopTimer = new HitStopEvent();
    [SerializeField]
    private HitStopEvent DeathHitStopTimer = new HitStopEvent();
    [SerializeField]
    private HitStopVariables hitStopVariables = new HitStopVariables();

    [System.Serializable]
    struct ShakeParametersCollection
    {
        public ShakeParameter ChargeAttack;
        public ShakeParameter NormalAttack;
        public ShakeParameter EnemyDeath;
    }

    [System.Serializable]
    struct ShakeParameter
    {
        public float ShakeAmount;
        public float ShakeDuration;
        public bool EnableShake;
    }

    [SerializeField] ShakeParametersCollection shakeParamameters = new ShakeParametersCollection();


    // Start is called before the first frame update
    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        HitStop();

        AttackNumberDEBUG = AttackNumber;
    }

    void OnTriggerEnter(Collider thisCollider)
    {
        if (thisCollider.tag == "Enemy")
        {
            enemyGameObject = thisCollider.gameObject;
            currentEnemyHealthScript = thisCollider.GetComponent<EnemyHealth>();

            if (currentEnemyHealthScript.lastAttackHitNumber == AttackNumber)
            {
                print("surplus hit blocked");
                return;
            }

            if (currentEnemyHealthScript.EnemyIsDead != true)
            {
                CalculateDamageAndHitstop();
                InflictDamage();

                currentEnemyHealthScript.lastAttackHitNumber = AttackNumber;
            }


        }
    }

    void HitStop()
    {
        if (HitStopTimer.IsFinished == false)
        {
            Time.timeScale = TimeScaleToUse;
        }

        else
        {
            Time.timeScale = 1;
        }
    }

    int CalculateDamageAndHitstop()
    {
        if (generalAnimationWeaponScript.isPerformingChargAttack == false)
        {
            damage = playerStats.Strength + PlayerEquiment.CurrentSwordOfPlayer.damage - currentEnemyHealthScript.defence;
            staggerDamage = damage;

            TimeScaleToUse = hitStopVariables.timeScaleAmountsPerAttack.RegularAttack;
            HitStopTimer.Duration = hitStopVariables.durationPerAttack.RegularAttack;

            if (shakeParamameters.NormalAttack.EnableShake == true)
            {
                cameraShakeScript.StartCamShakeCouretine(shakeParamameters.NormalAttack.ShakeDuration, shakeParamameters.NormalAttack.ShakeAmount);
                print("1");
            }

            return damage;
        }

        else if (generalAnimationWeaponScript.ChargeAttackCharged)
        {
            damage = playerStats.Strength + PlayerEquiment.CurrentSwordOfPlayer.chargeDamage - currentEnemyHealthScript.defence;
            staggerDamage = damage * chargeDamageMultiplier;

            TimeScaleToUse = hitStopVariables.timeScaleAmountsPerAttack.ChargedAttack;
            HitStopTimer.Duration = hitStopVariables.durationPerAttack.ChargedAttack;

            if (shakeParamameters.ChargeAttack.EnableShake == true)
            {
                cameraShakeScript.StartCamShakeCouretine(shakeParamameters.ChargeAttack.ShakeDuration, shakeParamameters.ChargeAttack.ShakeAmount);
                print("1");
            }

            return damage;
        }

        else
        {
            damage = playerStats.Strength + PlayerEquiment.CurrentSwordOfPlayer.chargeDamage / chargeDamagePenaltyMultiplier - currentEnemyHealthScript.defence;
            staggerDamage = damage / chargeDamagePenaltyMultiplier;

            TimeScaleToUse = hitStopVariables.timeScaleAmountsPerAttack.UnchargedAttack;
            HitStopTimer.Duration = hitStopVariables.durationPerAttack.RegularAttack;

            return damage;
        }
    }

    public void InflictDamage()
    {
        int clampedDamage = Mathf.Clamp(damage, 1, 9999999);
        int clampedStaggerDamage = Mathf.Clamp(staggerDamage, 1, 9999999);
        currentEnemyHealthScript.EnemyRecieveDamage(clampedDamage, staggerDamage);

        if (currentEnemyHealthScript.EnemyIsDead == true)
        {
            cameraShakeScript.StartCamShakeCouretine(shakeParamameters.EnemyDeath.ShakeDuration, shakeParamameters.EnemyDeath.ShakeAmount);
            print("aw yeah shake it baby");

            HitStopTimer.Duration = hitStopVariables.enemyDeathParams.Duration;
            TimeScaleToUse = hitStopVariables.enemyDeathParams.TimeScale;
            HitStopTimer.SetClock();

        }
    }
}
