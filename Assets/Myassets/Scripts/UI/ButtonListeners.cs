using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ButtonListeners : MonoBehaviour
{
    [SerializeField] Button ThisButton;
    public int index;

    private Button AlternativeButtonToSelect;
    private Transform InventoryParent;

    GameObject selectedGO;
    GameObject alternativeGO;
    ButtonListeners CurrentButtonListener;

    [SerializeField]
    private TimedEvent selectDelay = new TimedEvent();


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
            Clock = Time.unscaledTime + Duration;
        }

        public bool IsFinished => Time.unscaledTime >= Clock;
    }

    public void Start()
    {
        this.enabled = false;
    }

    public void SelectDelay()
    {

        if (selectedGO != null)
        {
            selectedGO.GetComponent<Button>().Select();
            CostumGameLoopPause.PauseLateUpdateLoopFunctionsSubscriber -= SelectDelay;
        }

        else
        {
            this.enabled = false;
            CostumGameLoopPause.PauseLateUpdateLoopFunctionsSubscriber -= SelectDelay;
        }

    }

    public void SelectButtonOnRight()
    {
        if (gameObject.GetComponent<Image>().sprite == null)
        {
            return;
        }

        foreach (Transform gameObjectTrans in PlayerInventory.inventoryParentObjectStatic.transform)
        {
            gameObjectTrans.gameObject.TryGetComponent<ButtonListeners>(out ButtonListeners CurrentButtonListener);

            if (CurrentButtonListener.index == index - 1)
            {
                alternativeGO = gameObjectTrans.gameObject;
            }

            if (CurrentButtonListener.index == index + 1)
            {

                if (gameObjectTrans.gameObject.GetComponent<Image>().sprite == null)
                {
                    selectedGO = alternativeGO;
                    selectDelay.SetClock();
                    CostumGameLoopPause.PauseLateUpdateLoopFunctionsSubscriber += SelectDelay;
                    break;
                }

                else if (GetComponent<Image>().sprite != null)
                {
                    selectedGO = gameObjectTrans.gameObject;
                    selectDelay.SetClock();
                    CostumGameLoopPause.PauseLateUpdateLoopFunctionsSubscriber += SelectDelay;
                    break;
                }
            }
        }
    }
}

