using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum TypeOfHealthNumber { playerDamage, enemyNormal, EnemyCrit, enemyFailedAttack, heal }

public class HealthNumberAnimation : MonoBehaviour
{
    [SerializeField] TextMeshPro number;

    [Header("Size")]

    [SerializeField] float riseSpeed;
    [SerializeField] int maxFontSize;
    [SerializeField] int maxBurstFontSize;
    float currentFontSize;

    private bool burstReached;


    [SerializeField]
    TimerScaled timeUntilShrinking = new TimerScaled();
    [SerializeField]
    TimerScaled timeUntilFade = new TimerScaled();
    [SerializeField]
    TimerScaled timeUntilDeactivate = new TimerScaled();
    [SerializeField]
    LerpEventUnscaled ShrinkingLerp = new LerpEventUnscaled();
    [SerializeField]
    LerpEventUnscaled burstLerpEvent = new LerpEventUnscaled();
    [SerializeField]
    LerpEventUnscaled OpacityLerpEvent = new LerpEventUnscaled();

    [SerializeField] Color numberColor;

    // Start is called before the first frame update
    void Start()
    {
        number.fontSize = maxFontSize;
        timeUntilShrinking.SetClock();
        timeUntilFade.SetClock();
        timeUntilDeactivate.SetClock();
        number.fontSize = maxFontSize;
        //CustomGameLoop.UpdateLoopFunctionsSubscriber += LerpSize;
    }

    public void InitNumber(int DamageNumber, TypeOfHealthNumber typeOfHealthNumber)
    {
        switch (typeOfHealthNumber)
        {
            case TypeOfHealthNumber.enemyNormal:

                CustomGameLoop.UpdateLoopFunctionsSubscriber += LerpOpacity;
                CustomGameLoop.UpdateLoopFunctionsSubscriber += LerpSize;
                CustomGameLoop.UpdateLoopFunctionsSubscriber += IncreaseHeight;

                number.color = Color.cyan;

                break;

            case TypeOfHealthNumber.EnemyCrit:

                CustomGameLoop.UpdateLoopFunctionsSubscriber += LerpBurst;
                CustomGameLoop.UpdateLoopFunctionsSubscriber += LerpOpacity;
                CustomGameLoop.UpdateLoopFunctionsSubscriber += LerpSize;
                CustomGameLoop.UpdateLoopFunctionsSubscriber += IncreaseHeight;

                number.color = Color.cyan;

                break;

            case TypeOfHealthNumber.enemyFailedAttack:

                CustomGameLoop.UpdateLoopFunctionsSubscriber += LerpOpacity;

                number.color = Color.cyan;

                break;

            case TypeOfHealthNumber.playerDamage:

                CustomGameLoop.UpdateLoopFunctionsSubscriber += LerpOpacity;
                CustomGameLoop.UpdateLoopFunctionsSubscriber += LerpSize;
                CustomGameLoop.UpdateLoopFunctionsSubscriber += IncreaseHeight;

                number.color = Color.red;
                break;

            case TypeOfHealthNumber.heal:

                CustomGameLoop.UpdateLoopFunctionsSubscriber += LerpOpacity;
                CustomGameLoop.UpdateLoopFunctionsSubscriber += LerpSize;
                CustomGameLoop.UpdateLoopFunctionsSubscriber += IncreaseHeight;

                number.color = Color.green;
                break;

            default:

                break;
        }


        number.fontSize = maxFontSize;
        timeUntilShrinking.SetClock();
        timeUntilFade.SetClock();
        number.fontSize = maxFontSize;
        timeUntilDeactivate.SetClock();
        CustomGameLoop.UpdateLoopFunctionsSubscriber += DisableTimer;

        numberColor = number.color;

        number.text = DamageNumber.ToString();
    }

    public void InitHealNumber()
    {

    }

    /*void Update()
    {
        LerpSize();
        IncreaseHeight();
        LerpBurst();
        LerpOpacity();
        DestroyTimer();
    }*/

    void LerpSize()
    {
        if (timeUntilShrinking.IsFinished == true)
        {
            ShrinkingLerp.Lerp();
            currentFontSize = Mathf.Lerp(maxFontSize, 0, ShrinkingLerp.LerpFloat);
            number.fontSize = currentFontSize;
        }
    }

    void IncreaseHeight()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + riseSpeed, transform.position.z);
    }

    void LerpBurst()
    {
        if (timeUntilShrinking.IsFinished == true)
            return;

        if (burstReached == false)
        {
            print("1");
            burstLerpEvent.Lerp();
            currentFontSize = Mathf.Lerp(maxFontSize, maxBurstFontSize, burstLerpEvent.LerpFloat);
            number.fontSize = currentFontSize;

            if (maxBurstFontSize <= number.fontSize)
            {

                burstLerpEvent.ResetLerp();
                burstReached = true;
            }
        }

        else
        {
            print("2");
            burstLerpEvent.Lerp();
            currentFontSize = Mathf.Lerp(maxBurstFontSize, maxFontSize, burstLerpEvent.LerpFloat);
            number.fontSize = currentFontSize;
        }
    }

    void LerpOpacity()
    {
        if (timeUntilFade.IsFinished == true)
        {
            OpacityLerpEvent.Lerp();
            numberColor.a = Mathf.Lerp(number.color.a, 0, OpacityLerpEvent.LerpFloat);
            number.color = numberColor;
        }
    }

    void DisableTimer()
    {
        if (timeUntilDeactivate.IsFinished == true)
        {
            gameObject.SetActive(false);

            ShrinkingLerp.ResetLerp();
            burstLerpEvent.ResetLerp();
            OpacityLerpEvent.ResetLerp();
            numberColor.a = 255;
            number.color = numberColor;

        }
    }

    void OnDestroy()
    {
        CustomGameLoop.UpdateLoopFunctionsSubscriber -= LerpBurst;
        CustomGameLoop.UpdateLoopFunctionsSubscriber -= LerpOpacity;
        CustomGameLoop.UpdateLoopFunctionsSubscriber -= LerpSize;
        CustomGameLoop.UpdateLoopFunctionsSubscriber -= IncreaseHeight;
        CustomGameLoop.UpdateLoopFunctionsSubscriber -= DisableTimer;
    }

    void OnDisable()
    {
        CustomGameLoop.UpdateLoopFunctionsSubscriber -= LerpBurst;
        CustomGameLoop.UpdateLoopFunctionsSubscriber -= LerpOpacity;
        CustomGameLoop.UpdateLoopFunctionsSubscriber -= LerpSize;
        CustomGameLoop.UpdateLoopFunctionsSubscriber -= IncreaseHeight;
        CustomGameLoop.UpdateLoopFunctionsSubscriber -= DisableTimer;
    }
}
