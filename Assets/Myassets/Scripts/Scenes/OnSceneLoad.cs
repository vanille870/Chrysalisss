using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnSceneLoad : MonoBehaviour
{
    [System.Serializable]
    public struct TimerBeforeControl
    {
        public float Duration;
        private float Clock;

        public TimerBeforeControl(float duration, float time = 0f)
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
    TimerBeforeControl timerBeforeControl = new TimerBeforeControl();

    // Start is called before the first frame update
    void Awake()
    {
        timerBeforeControl.SetClock();
    }

    void Update()
    {
        if (timerBeforeControl.IsFinished)
        {
            SceneManager.SetActiveScene(gameObject.scene);
            InputManager.EnableControls();
            GameMaster.gameMasterSingleton.playerController.enabled = true;
            Destroy(gameObject);
        }
    }
}
