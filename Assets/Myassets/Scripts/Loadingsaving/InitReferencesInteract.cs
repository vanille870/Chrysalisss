using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitStaticReferencesStorageInteract : MonoBehaviour
{
    public StaticReferencesStorage StaticReferencesStorageSO;

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
        if (TryGetComponent<Sign>(out Sign sign))
        {
            sign.interactScript = StaticReferencesStorageSO.interactScriptReference;
            sign.playerPos = StaticReferencesStorageSO.playerTrans;

            sign.textBoxScript = StaticReferencesStorageSO.textBoxScript;
        }

        else if (TryGetComponent<ItemPickup>(out ItemPickup itemPickup))
        {
            itemPickup.InitStaticReferencesStorage
            (
                StaticReferencesStorageSO.playerInventory,
                StaticReferencesStorageSO.playerTrans,
                StaticReferencesStorageSO.textBoxScript
            );
        }

        else if (TryGetComponent<FallInPit>(out FallInPit fallInPit))
        {
            fallInPit.playerGO = StaticReferencesStorageSO.playerGO;
            fallInPit.playerAnim = StaticReferencesStorageSO.mainCharAnimationScript;
            fallInPit.inputManagerScript = StaticReferencesStorageSO.inputManagerScript;
            fallInPit.playerController = StaticReferencesStorageSO.playerController;
            fallInPit.originalPlayerRot = StaticReferencesStorageSO.playerGO.transform.rotation;
            fallInPit.playerVisibilityParent = StaticReferencesStorageSO.playerVisibilityParent;
        }

    }
}
