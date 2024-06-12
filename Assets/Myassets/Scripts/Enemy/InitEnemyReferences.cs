using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitEnemyReferences : MonoBehaviour
{

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
            basic_Enemy_AIScript.playerController = GameMaster.gameMasterSingleton.playerController;
            basic_Enemy_AIScript.PlayerPoint = GameMaster.gameMasterSingleton.playerTrans;
            GetComponentInChildren<Enemy_Animation>().PlayerPoint = GameMaster.gameMasterSingleton.playerTrans;

            enemyHitbox.playerConditionStatsScript = GameMaster.gameMasterSingleton.playerConditionStatsScript;
            enemyHitbox.mainCharAnimationScript = GameMaster.gameMasterSingleton.mainCharAnimationScript;
            enemyHitbox.movementScript = GameMaster.gameMasterSingleton.PlayerMovementReference;
            enemyHitbox.playerPoint = GameMaster.gameMasterSingleton.playerTrans;
            CustomGameLoop.UpdateLoopFunctionsSubscriber -= UpdateFunction;
        }
    }
}
