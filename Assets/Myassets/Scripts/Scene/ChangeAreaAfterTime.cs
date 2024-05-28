using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SpawPoints;
using System;

public class ChangeAreaAfterTime : MonoBehaviour
{
    [SerializeField] Transform playerSpawnPos;
    [SerializeField] Transform playerParent;
    [SerializeField] PlayerSpawner playerSpawner;

    [Serializable]
    public struct TimedEvent
    {
        [SerializeField]
        [Range(0f, 4f)]
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
    private TimedEvent timer = new TimedEvent();

    void Awake()
    {
        timer.SetClock();
        CustomGameLoop.UpdateLoopFunctionsSubscriber += Timer;
    }

    public void Timer()
    {
        if (timer.IsFinished)
        {
            playerParent.position = SpawPoints.Field.fromInit;
            CustomGameLoop.UpdateLoopFunctionsSubscriber -= Timer;

            SceneManager.LoadScene("Enemy_Testing", LoadSceneMode.Additive);
        }
    }

    
}
