using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

using CinePerlin = Cinemachine.CinemachineBasicMultiChannelPerlin;
using System;

public class CameraShake : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera cinemachineVirtualCamera;
    CinePerlin PerlinNoise;

    public float CurrentShakeAmount;
    [SerializeField] bool IsCouritineFinished;

    Coroutine CurrentCamShakeCoroutine;


    // Start is called before the first frame update
    void Start()
    {
        PerlinNoise = cinemachineVirtualCamera.GetCinemachineComponent<CinePerlin>();
        PerlinNoise.m_AmplitudeGain = 0;

        CurrentCamShakeCoroutine = StartCoroutine(TriggerCamShake(0, 0));
    }

    void Update()
    {
        CurrentShakeAmount = PerlinNoise.m_AmplitudeGain;

        if (CurrentCamShakeCoroutine == null)
        {
            print("couritine is null");
        }
    }

    IEnumerator TriggerCamShake(float Duration, float Strength)
    {
        // Assign total shake
        PerlinNoise.m_AmplitudeGain = Strength;
        // Timer start at 0
        float t = 0;

        Duration = MathF.Max(Duration, 0.001f);

        // Loop while timer is not complete
        while (t < 1)
        {
            // Convert the duration to a 0 to 1, so Duration's worth of delta time.
            t += Time.unscaledDeltaTime / Duration;

            // We're starting at full strength and reducing, so flip the 0-1 value so it becomes 1 to 0.
            PerlinNoise.m_AmplitudeGain = Strength * (1f - t);

            // Wait a frame
            yield return null;
        }

        // Make sure the amplitude finishes at 0.
        PerlinNoise.m_AmplitudeGain = 0;
    }

    public void StartCamShakeCouretine(float duration, float strength)
    {

        StopCoroutine(CurrentCamShakeCoroutine);
        CurrentCamShakeCoroutine = StartCoroutine(TriggerCamShake(duration, strength));
    }
}
