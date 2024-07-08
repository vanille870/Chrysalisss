using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class On_enemy_hit : MonoBehaviour
{
    public GeneralAnimationWeapon generalAnimationWeaponScript;
    public CameraShake cameraShakeScript;
    public PlayerEquiment playerEquimentScript;
    public PlayerStats playerStats;
    [SerializeField] HealthNumberManager damageNumberManager;


    [Header("DamageNumber setup")]
    [SerializeField] bool fadesOut;
    [SerializeField] bool shrinks;
    [SerializeField] bool bursts;
    [SerializeField] bool rises;
    [SerializeField] Vector3 enemyPosition;


    public static int AttackNumber = 0;
    [SerializeField] int AttackNumberDEBUG;


    public ParticleSystem enemyHiteffect;
    private GameObject enemyGameObject;

    EnemyHealth currentEnemyHealthScript;

    public int damage;
    public int staggerDamage;
    public int chargeDamagePenaltyMultiplier;
    [SerializeField] int chargeDamageMultiplier;
    [SerializeField] float DeathHitStopMultiplier;

    public float TimeScaleToUse;

    int swordAttackPower;
    Sword currentSword;
    SwordAttackTypes CurrentSwordAttackTypes;
    NormalAttack currentNormalAttack;
    ChargedAttack CurrentChargedAttack;
    FailedAttack CurrentfailedAttack;



    [System.Serializable]
    public struct HitStopEvent
    {
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

    public bool TestTimer;
    private Vector3 contactPoint;

    void OnEnable()
    {
        CustomGameLoop.UpdateLoopFunctionsSubscriber += UpdateHitStop;
    }

    void OnDisable()
    {
        CustomGameLoop.UpdateLoopFunctionsSubscriber -= UpdateHitStop;
    }


    // Update is called once per frame
    void UpdateHitStop()
    {
        HitStop();
        TestTimer = HitStopTimer.IsFinished;
    }

    void OnTriggerEnter(Collider thisCollider)
    {
        if (thisCollider.tag == "Enemy")
        {
            enemyGameObject = thisCollider.gameObject;
            currentEnemyHealthScript = thisCollider.GetComponent<EnemyHealth>();
            enemyPosition = thisCollider.transform.position;
            contactPoint = thisCollider.ClosestPointOnBounds(this.transform.position);

            /*GameObject GO = GameObject.CreatePrimitive(PrimitiveType.Cube);
            GO.transform.position = contactPoint;
            GO.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);*/

            if (currentEnemyHealthScript.lastAttackHitNumber == AttackNumber)
            {
                print("surplus hit blocked");
                return;
            }

            if (currentEnemyHealthScript.EnemyIsDead != true)
            {
                GetEquipmntInfo();
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
            CustomGameLoop.LateupdateLoopFunctionsSubscriber -= UpdateHitStop;
        }
    }

    int CalculateDamageAndHitstop()
    {

        if (generalAnimationWeaponScript.isPerformingChargAttack == false)
        {
            damage = currentNormalAttack.damage + playerStats.playerStatSet.strength - currentEnemyHealthScript.defence;
            staggerDamage = currentNormalAttack.staggerDamage;

            TimeScaleToUse = hitStopVariables.timeScaleAmountsPerAttack.RegularAttack;
            HitStopTimer.Duration = hitStopVariables.durationPerAttack.RegularAttack;

            if (shakeParamameters.NormalAttack.EnableShake == true)
            {
                cameraShakeScript.StartCamShakeCouretine(shakeParamameters.NormalAttack.ShakeDuration, shakeParamameters.NormalAttack.ShakeAmount);
            }

            print(damage);
            damageNumberManager.InstantiateHealthNumber(damage, enemyPosition, TypeOfHealthNumber.enemyNormal);
            return damage;
        }

        else if (generalAnimationWeaponScript.ChargeAttackCharged)
        {
            damage = CurrentChargedAttack.damage + playerStats.playerStatSet.strength - currentEnemyHealthScript.defence;
            staggerDamage = CurrentChargedAttack.staggerDamage;

            TimeScaleToUse = hitStopVariables.timeScaleAmountsPerAttack.ChargedAttack;
            HitStopTimer.Duration = hitStopVariables.durationPerAttack.ChargedAttack;

            if (shakeParamameters.ChargeAttack.EnableShake == true)
            {
                cameraShakeScript.StartCamShakeCouretine(shakeParamameters.ChargeAttack.ShakeDuration, shakeParamameters.ChargeAttack.ShakeAmount);
            }

            print(damage);
            damageNumberManager.InstantiateHealthNumber(damage, enemyPosition, TypeOfHealthNumber.EnemyCrit);
            return damage;
        }

        else
        {
            damage = CurrentfailedAttack.damage + playerStats.playerStatSet.strength - currentEnemyHealthScript.defence;
            staggerDamage = CurrentfailedAttack.staggerDamage;

            TimeScaleToUse = hitStopVariables.timeScaleAmountsPerAttack.UnchargedAttack;
            HitStopTimer.Duration = hitStopVariables.durationPerAttack.RegularAttack;

            print(damage);
            damageNumberManager.InstantiateHealthNumber(damage, enemyPosition, TypeOfHealthNumber.enemyFailedAttack);
            return damage;
        }
    }

    public void InflictDamage()
    {
        int clampedDamage = Mathf.Clamp(damage, 1, 9999999);
        int clampedStaggerDamage = Mathf.Clamp(staggerDamage, 1, 9999999);
        currentEnemyHealthScript.EnemyRecieveDamage(clampedDamage, staggerDamage, contactPoint);

        if (currentEnemyHealthScript.EnemyIsDead == true)
        {
            cameraShakeScript.StartCamShakeCouretine(shakeParamameters.EnemyDeath.ShakeDuration, shakeParamameters.EnemyDeath.ShakeAmount);
            print("aw yeah shake it baby");

            HitStopTimer.Duration = hitStopVariables.enemyDeathParams.Duration;
            TimeScaleToUse = hitStopVariables.enemyDeathParams.TimeScale;
            HitStopTimer.SetClock();
            CustomGameLoop.UpdateLoopFunctionsSubscriber += UpdateHitStop;
        }

        else
        {
            CustomGameLoop.UpdateLoopFunctionsSubscriber += UpdateHitStop;
            HitStopTimer.SetClock();
        }
    }

    public void GetEquipmntInfo()
    {
        currentSword = playerEquimentScript.CurrentSwordOfPlayer;
        CurrentSwordAttackTypes = currentSword.swordAttackTypes;
        currentNormalAttack = CurrentSwordAttackTypes.normalAttack;
        CurrentChargedAttack = CurrentSwordAttackTypes.chargedAttack;
        CurrentfailedAttack = CurrentSwordAttackTypes.failedAttack;


    }
}
