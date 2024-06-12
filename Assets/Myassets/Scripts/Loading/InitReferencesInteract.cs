using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitReferencesInteract : MonoBehaviour
{
    GameMaster singleton;

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
    private TimedEvent WaitUntilLoading = new TimedEvent();

    void Awake()
    {
        WaitUntilLoading.SetClock();
        CustomGameLoop.UpdateLoopFunctionsSubscriber += UpdateClock;
    }

    // Start is called before the first frame update
    void UpdateClock()
    {
        if (WaitUntilLoading.IsFinished)
        {
            InitReference();
            CustomGameLoop.UpdateLoopFunctionsSubscriber -= UpdateClock;
        }
    }

    void InitReference()
    {
        singleton = GameMaster.gameMasterSingleton;

        if (TryGetComponent<Sign>(out Sign sign))
        {
            sign.interactScript = singleton.interactScript;
            sign.playerPos = singleton.playerTrans;

            sign.textBoxScript = singleton.textBoxScript;
        }

        else if (TryGetComponent<ItemPickup>(out ItemPickup itemPickup))
        {
            itemPickup.InitReferences
            (
                singleton.playerInventory,
                singleton.playerTrans,
                singleton.textBoxScript
            );
        }

        else if (TryGetComponent<FallInPit>(out FallInPit fallInPit))
        {
            fallInPit.playerGO = singleton.playerGO;
            fallInPit.playerAnim = singleton.mainCharAnimationScript;
            fallInPit.inputManagerScript = singleton.inputManagerScript;
            fallInPit.playerController = singleton.playerController;
            fallInPit.originalPlayerRot = singleton.playerGO.transform.rotation;
        }

    }
}
