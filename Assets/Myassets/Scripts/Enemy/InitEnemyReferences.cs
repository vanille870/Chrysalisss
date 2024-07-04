using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitEnemyStaticReferencesStorage : MonoBehaviour
{
    public StaticReferencesStorage StaticReferencesStorageSO;

    [System.Serializable]
    public struct TimedEvent
    {
        [SerializeField]
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
    private TimedEvent WaitUntilLoading = new TimedEvent();

    [SerializeField] Basic_Enemy_AI basic_Enemy_AIScript;
    [SerializeField] EnemyHitbox enemyHitbox;

    // Start is called before the first frame update
    void Start()
    {
        WaitUntilLoading.SetClock();
        CustomGameLoop.UpdateLoopFunctionsSubscriber += UpdateFunction;
    }

    void UpdateFunction()
    {
        if (WaitUntilLoading.IsFinished == true)
        {
            basic_Enemy_AIScript.playerController = StaticReferencesStorageSO.playerController;
            basic_Enemy_AIScript.PlayerPoint = StaticReferencesStorageSO.playerPoint;
            GetComponentInChildren<Enemy_Animation>().PlayerPoint = StaticReferencesStorageSO.playerTrans;

            enemyHitbox.playerConditionStatsScript = StaticReferencesStorageSO.playerStatsReference;
            enemyHitbox.mainCharAnimationScript = StaticReferencesStorageSO.mainCharAnimationScript;
            enemyHitbox.movementScript = StaticReferencesStorageSO.PlayerMovementReference;
            enemyHitbox.playerPoint = StaticReferencesStorageSO.playerPoint;
            CustomGameLoop.UpdateLoopFunctionsSubscriber -= UpdateFunction;
        }
    }
}
