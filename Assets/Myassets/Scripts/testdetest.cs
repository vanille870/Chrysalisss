using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testdetest : MonoBehaviour
{
    public bool boooool;
    public float inttt;


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
    private TimedEvent TrackTimer = default;

    // Start is called before the first frame update
    void Start()
    {
        
        TrackTimer.SetClock();

        
    }
}
