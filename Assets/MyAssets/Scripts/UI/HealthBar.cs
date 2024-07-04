using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public static Slider playerHealthBar;
    public static Slider AdjustingHealthBar;
    public float adjustSpeedInspector;
    public float durationTimer;
    [SerializeField] static float adjustSpeed;
    [SerializeField] GameObject AdjustSliderGO;

    [SerializeField]
    static TimerScaled TimeBeforeDarkDeplete = new TimerScaled();

    static bool IsDepletingAdjustBar;

    public void Awake()
    {
        playerHealthBar = gameObject.GetComponent<Slider>();
        AdjustingHealthBar = AdjustSliderGO.GetComponent<Slider>();

        adjustSpeed = adjustSpeedInspector;
        TimeBeforeDarkDeplete.Duration = durationTimer;
    }

    public static void SetUpHealthBar(int MaxHP, int currentHP)
    {
        print("healtbar setup");
        playerHealthBar.maxValue = MaxHP;
        playerHealthBar.value = currentHP;

        AdjustingHealthBar.maxValue = MaxHP;
        AdjustingHealthBar.value = currentHP;
    }

    public static void UpdateHealthBar(int damage)
    {
        playerHealthBar.value -= damage;

        if (IsDepletingAdjustBar == false)
        {
            CustomGameLoop.UpdateLoopFunctionsSubscriber += DepleteDarkBar;
            IsDepletingAdjustBar = true;
        }


        TimeBeforeDarkDeplete.SetClock();
    }

    static void DepleteDarkBar()
    {
        if (TimeBeforeDarkDeplete.IsFinished)
        {
            AdjustingHealthBar.value -= Time.deltaTime * adjustSpeed;

            if (AdjustingHealthBar.value <= playerHealthBar.value)
            {
                AdjustingHealthBar.value = playerHealthBar.value;
                CustomGameLoop.UpdateLoopFunctionsSubscriber -= DepleteDarkBar;
                IsDepletingAdjustBar = false;
            }
        }
    }
}
