using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum EntityType { player, enemy }

public class DamageNumberAnimation : MonoBehaviour
{
    [SerializeField] TextMeshPro damageNumber;

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
    LerpEvent ShrinkingLerp = new LerpEvent();
    [SerializeField]
    LerpEvent burstLerpEvent = new LerpEvent();
    [SerializeField]
    LerpEvent OpacityLerpEvent = new LerpEvent();

    [SerializeField] Color numberColor;

    // Start is called before the first frame update
    void Start()
    {
        damageNumber.fontSize = maxFontSize;
        timeUntilShrinking.SetClock();
        timeUntilFade.SetClock();
        timeUntilDeactivate.SetClock();
        damageNumber.fontSize = maxFontSize;
        //CustomGameLoop.UpdateLoopFunctionsSubscriber += LerpSize;
    }

    public void InitDamageNumber(int DamageNumber, EntityType entity, bool hasBurst, bool fadesOut, bool shrinks, bool Rises)
    {
        if (hasBurst)
            CustomGameLoop.UpdateLoopFunctionsSubscriber += LerpBurst;

        if (fadesOut)
            CustomGameLoop.UpdateLoopFunctionsSubscriber += LerpOpacity;

        if (shrinks)
            CustomGameLoop.UpdateLoopFunctionsSubscriber += LerpSize;

        if (Rises)
            CustomGameLoop.UpdateLoopFunctionsSubscriber += IncreaseHeight;

        if (entity == EntityType.player)
        {
            damageNumber.color = Color.red;
            numberColor = Color.red;
        }

        else if (entity == EntityType.enemy)
        {
            damageNumber.color = Color.cyan;
            numberColor = Color.cyan;
        }



        damageNumber.fontSize = maxFontSize;
        timeUntilShrinking.SetClock();
        timeUntilFade.SetClock();
        damageNumber.fontSize = maxFontSize;
        timeUntilDeactivate.SetClock();
        CustomGameLoop.UpdateLoopFunctionsSubscriber += DisableTimer;

        damageNumber.text = DamageNumber.ToString();
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
            damageNumber.fontSize = currentFontSize;
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
            damageNumber.fontSize = currentFontSize;

            if (maxBurstFontSize <= damageNumber.fontSize)
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
            damageNumber.fontSize = currentFontSize;
        }
    }

    void LerpOpacity()
    {
        if (timeUntilFade.IsFinished == true)
        {
            OpacityLerpEvent.Lerp();
            numberColor.a = Mathf.Lerp(damageNumber.color.a, 0, OpacityLerpEvent.LerpFloat);
            damageNumber.color = numberColor;
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
            damageNumber.color = numberColor;

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
