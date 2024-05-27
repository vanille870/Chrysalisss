using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;
using CinePerlin = Cinemachine.CinemachineBasicMultiChannelPerlin;


public class CameraShake : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera cinemachineVirtualCamera;
    CinePerlin PerlinNoise;

    public float CurrentShakeAmount;
    [SerializeField] bool IsCouritineFinished;

    [HideInInspector]
    public float Strength;
    [HideInInspector]
    public float Duration;

    bool ShakeStarted;
    float t = 0;

    void OnDisable()
    {
        print("disbaled");
        CustomGameLoop.UpdateLoopFunctionsSubscriber -= CamShakeUpdate;
    }

    // Start is called before the first frame update
    void Start()
    {
        PerlinNoise = cinemachineVirtualCamera.GetCinemachineComponent<CinePerlin>();
        PerlinNoise.m_AmplitudeGain = 0;
    }

    void CamShakeUpdate()
    {
        if (ShakeStarted == true)
        {
            // Assign total shake
            PerlinNoise.m_AmplitudeGain = Strength;
            // Timer start at 0
            t = 0;

            Duration = MathF.Max(Duration, 0.001f);
            ShakeStarted = false;
        }

        // Loop while timer is not complete
        if (t < 1)
        {
            // Convert the duration to a 0 to 1, so Duration's worth of delta time.
            t += Time.unscaledDeltaTime / Duration;

            // We're starting at full strength and reducing, so flip the 0-1 value so it becomes 1 to 0.
            PerlinNoise.m_AmplitudeGain = Strength * (1f - t);

            CurrentShakeAmount = PerlinNoise.m_AmplitudeGain;

            print(CurrentShakeAmount);

            return;
        }

        // Make sure the amplitude finishes at 0.
        PerlinNoise.m_AmplitudeGain = 0;
        //CustomGameLoop.UpdateLoopFunctionsSubscriber -= CamShakeUpdate;
    }

    public void StartCamShakeCouretine(float inputDuration, float inputStrength)
    {
        Duration = inputDuration;
        Strength = inputStrength;
        ShakeStarted = true;

        CustomGameLoop.UpdateLoopFunctionsSubscriber += CamShakeUpdate;

    }
}
