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
    [SerializeField] Image AdjustingHealthBarSpriteInspector;
    [SerializeField] static Image AdjustingHealthBarSprite;
    [SerializeField] Image HealthBarSpriteInspector;
    [SerializeField] static Image HealthBarSprite;
    public float adjustSpeedInspector;
    public float durationTimer;
    [SerializeField] static float adjustSpeed;
    [SerializeField] GameObject AdjustSliderGO;

    [SerializeField]
    static TimerScaled TimeBeforeDarkDeplete = new TimerScaled();

    static bool IsDepletingAdjustBar;
    static bool IsAdjustingBar;

    [SerializeField] StatsStorage statsStorage;

    [SerializeField] Color color;


    public void Awake()
    {
        playerHealthBar = gameObject.GetComponent<Slider>();
        AdjustingHealthBar = AdjustSliderGO.GetComponent<Slider>();

        adjustSpeed = adjustSpeedInspector;
        TimeBeforeDarkDeplete.Duration = durationTimer;

        AdjustingHealthBarSprite = AdjustingHealthBarSpriteInspector;
        HealthBarSprite = HealthBarSpriteInspector;

        playerHealthBar.onValueChanged.AddListener(delegate {ChangColorOfSlider(); });
    }

    public static void SetUpHealthBar(int MaxHP, int currentHP)
    {
        print("healtbar setup");
        playerHealthBar.maxValue = MaxHP;
        playerHealthBar.value = currentHP;

        AdjustingHealthBar.maxValue = MaxHP;
        AdjustingHealthBar.value = currentHP;
    }

    public static void UpdateHealthBar(int amount, bool IsDamage)
    {
        if (IsDamage)
        {
            AdjustingHealthBarSprite.color = new Color32(20, 0, 0, 255);

            playerHealthBar.value -= amount;
            CustomGameLoop.UpdateLoopFunctionsSubscriber += HealthBarCatchUp;
            IsDepletingAdjustBar = true;
            IsAdjustingBar = true;

            if (IsDepletingAdjustBar)
            {
                AdjustingHealthBar.value = playerHealthBar.value + amount;
            }
        }

        else
        {
            AdjustingHealthBarSprite.color = Color.green;

            if (IsDepletingAdjustBar)
            {
                AdjustingHealthBar.value = playerHealthBar.value + amount;
            }

            AdjustingHealthBar.value += amount;
            CustomGameLoop.UpdateLoopFunctionsSubscriber += HealthBarCatchUp;
            IsDepletingAdjustBar = false;
            IsAdjustingBar = true;

        }

        TimeBeforeDarkDeplete.SetClock();
    }

    static void HealthBarCatchUp()
    {
        if (TimeBeforeDarkDeplete.IsFinished)
        {
            if (IsDepletingAdjustBar)
            {
                if (AdjustingHealthBar.value >= playerHealthBar.value)
                {
                    AdjustingHealthBar.value -= Time.deltaTime * adjustSpeed;

                    if (AdjustingHealthBar.value <= playerHealthBar.value)
                    {
                        CustomGameLoop.UpdateLoopFunctionsSubscriber -= HealthBarCatchUp;
                        AdjustingHealthBar.value = playerHealthBar.value;
                        IsDepletingAdjustBar = false;
                        IsAdjustingBar = false;
                    }
                }
            }

            else
            {
                print("ayooooooooo");
                playerHealthBar.value += Time.deltaTime * adjustSpeed;

                if (playerHealthBar.value >= AdjustingHealthBar.value)
                {
                    playerHealthBar.value = AdjustingHealthBar.value;
                    CustomGameLoop.UpdateLoopFunctionsSubscriber -= HealthBarCatchUp;
                    IsAdjustingBar = false;
                }
            }
        }
    }

    static void ChangColorOfSlider()
    {
        float percentage = playerHealthBar.value / playerHealthBar.maxValue;

        HealthBarSprite.color = new Color(percentage, 0, 0 ,255);
    }
}
