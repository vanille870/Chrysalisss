using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class On_enemy_hit : MonoBehaviour
{
    public PlayerStats playerStats;
    public GeneralAnimationWeapon generalAnimationWeaponScript;

    public static int AttackNumber = 0;

    public ParticleSystem enemyHiteffect;
    private GameObject enemyGameObject;

    EnemyHealth currentEnemyHealthScript;

    public int damage;
    public int staggerDamage;
    public int chargeDamagePenaltyMultiplier;
    float TimeScaleToUse;

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
    public struct HitStopVariables
    {
        public TimeScaleAmountsPerAttack timeScaleAmountsPerAttack;
        public DurationPerAttack durationPerAttack;
    }

    [SerializeField]
    private HitStopEvent HitStopTimer = new HitStopEvent();

    [SerializeField]
    private HitStopVariables hitStopVariables = new HitStopVariables();





    // Start is called before the first frame update
    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        HitStop();
    }

    void OnTriggerEnter(Collider thisCollider)
    {
        if (thisCollider.tag == "Enemy")
        {
            enemyGameObject = thisCollider.gameObject;
            enemyHiteffect = enemyGameObject.GetComponentInChildren<ParticleSystem>();
            enemyHiteffect.Play();
            currentEnemyHealthScript = thisCollider.GetComponent<EnemyHealth>();


            CalculateDamageAndHitstop();
            InflictDamage();

            HitStopTimer.SetClock();
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


            return damage;
        }

        else if (generalAnimationWeaponScript.ChargeAttackCharged)
        {
            damage = playerStats.Strength + PlayerEquiment.CurrentSwordOfPlayer.chargeDamage - currentEnemyHealthScript.defence;
            staggerDamage = damage * 2;

            TimeScaleToUse = hitStopVariables.timeScaleAmountsPerAttack.ChargedAttack;
            HitStopTimer.Duration = hitStopVariables.durationPerAttack.ChargedAttack;

            return damage;
        }

        else
        {
            damage = playerStats.Strength + PlayerEquiment.CurrentSwordOfPlayer.chargeDamage / chargeDamagePenaltyMultiplier - currentEnemyHealthScript.defence;
            staggerDamage = damage / 2;

            TimeScaleToUse = hitStopVariables.timeScaleAmountsPerAttack.UnchargedAttack;
            HitStopTimer.Duration = hitStopVariables.durationPerAttack.RegularAttack;

            return damage;
        }
    }

    public void InflictDamage()
    {
        int clampedDamage = Mathf.Clamp(damage, 1, 9999999);
        int clampedStaggerDamage = Mathf.Clamp(staggerDamage, 1, 9999999);
        currentEnemyHealthScript.EnemyRecieveDamage(clampedDamage, staggerDamage, AttackNumber);
    }
}
