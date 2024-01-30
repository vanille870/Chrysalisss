using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class On_enemy_hit : MonoBehaviour
{
    public PlayerStats playerStats;

    public static int AttackNumber = 0;

    public ParticleSystem enemyHiteffect;
    private GameObject enemyGameObject;

    EnemyHealth currentEnemyHealthScript;

    public int damage;
    public int staggrDamage;

    [Range(0.0f, 1.0f)]
    public float hitSTopTimeScale;


    [System.Serializable]
    public struct HitStopEvent
    {
        [SerializeField]
        [Range(0.0f, 10.0f)]
        private float Duration;
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

    [SerializeField]
    private HitStopEvent HitStopTimer = new HitStopEvent();

    // Start is called before the first frame update
    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        HitStop();
    }

    void PlayEnemyHitEffect()
    {

    }

    void OnTriggerEnter(Collider thisCollider)
    {
        if (thisCollider.tag == "Enemy")
        {
            enemyGameObject = thisCollider.gameObject;
            enemyHiteffect = enemyGameObject.GetComponentInChildren<ParticleSystem>();
            enemyHiteffect.Play();
            HitStopTimer.SetClock();
            currentEnemyHealthScript = thisCollider.GetComponent<EnemyHealth>();

            
            CalculateDamage();
            InflictDamage();
        }
    }

    void HitStop()
    {
        if (HitStopTimer.IsFinished == false)
        {
            Time.timeScale = hitSTopTimeScale;
        }

        else
        {
            Time.timeScale = 1;
        }
    }

    int CalculateDamage()
    {
        damage = playerStats.Strength + PlayerEquiment.CurrentSwordOfPlayer.damage - currentEnemyHealthScript.defence;

        return damage;
    }

    public void InflictDamage()
    {
        int clampedDamage = Mathf.Clamp(damage, 1, 9999999);
        currentEnemyHealthScript.EnemyRecieveDamage(clampedDamage, clampedDamage, AttackNumber);
    }
}
