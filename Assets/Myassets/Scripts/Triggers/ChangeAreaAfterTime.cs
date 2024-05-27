using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeAreaAfterTime : MonoBehaviour
{
    [System.Serializable]
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
            Clock = Time.time + 0.5f;
        }

        public bool IsFinished => Time.time >= Clock;
    }

    [SerializeField]
    private TimedEvent timer = new TimedEvent();

    void Awake()
    {
        print("hiiiiiiiiiiiii");
        timer.SetClock();
        CustomGameLoop.UpdateLoopFunctionsSubscriber += Timer;
    }

    public void Timer()
    {
        if (timer.IsFinished)
        {
            print("loading") ;
            SceneManager.LoadScene("Enemy_Testing");
            CustomGameLoop.UpdateLoopFunctionsSubscriber -= Timer;
        }
    }
}
