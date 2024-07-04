using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine.Events;


[Serializable]
public class InventoryItemInstance
{
    public InventoryItemsData inventoryItemsDataSO;
}

[Serializable]
public class InventoryItemSlot
{
    public InventoryItemsData inventoryItemsDataSO;
    public GameObject slotGO;
    public bool hasItem = false;

    private UnityAction reorderItems = PlayerInventory.ReorderItems;
    private Image itemImage;
    private TextMeshProUGUI GOtext;
    private Button GObutton;


    public InventoryItemSlot(GameObject inputGO)
    {
        slotGO = inputGO;

        itemImage = slotGO.GetComponent<Image>();
        GObutton = slotGO.GetComponent<Button>();
        GOtext = slotGO.GetComponentInChildren<TextMeshProUGUI>();
    }

    public void UpdateGO()
    {
        itemImage.sprite = inventoryItemsDataSO.itemImage;
        GOtext.text = inventoryItemsDataSO.itemName;

        inventoryItemsDataSO.InventoryItemsDataSetup();
        GObutton.onClick.AddListener(() => inventoryItemsDataSO.itemFunction(inventoryItemsDataSO.effectMagintude));
        GObutton.onClick.AddListener(DisableGO);
        slotGO.SetActive(true);
        itemImage.enabled = true;
    }

    public void DisableGO()
    {
        hasItem = false;
        GOtext.text = null;
        itemImage.enabled = false;
        itemImage.sprite = null;
        GObutton.onClick.RemoveAllListeners();
        reorderItems();
    }

    public void ReadyGO()
    {
        if (itemImage.sprite == null)
            itemImage.enabled = false;

    }
}

public class PostitionToVector3 : PropertyAttribute { }
public class ItemFunctionPicker : PropertyAttribute { }

[Serializable]
public class Saveable2stateObject
{
    public string name;
    public int ID;
    public bool Open;
}

namespace SpawnByLocation
{
    [Serializable]
    public class Spawns
    {
        public int ID;

        [PostitionToVector3]
        public Vector3 Location;

        public Spawns(Vector3 inputLoc)
        {
            Location = inputLoc;
        }
    }
}

[System.Serializable]
public struct TimerScaled
{
    public float Duration;
    private float Clock;

    public TimerScaled(float duration, float time = 0f)
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

[System.Serializable]
public struct LerpEvent
{
    [SerializeField]
    public float DurationMultiplier;
    public float LerpFloat;

    public LerpEvent(float durationmult, float time = 0f)
    {
        DurationMultiplier = durationmult;
        LerpFloat = time;
    }

    public void Lerp()
    {
        LerpFloat += Time.deltaTime * DurationMultiplier;
    }

    public void ResetLerp()
    {
        LerpFloat = 0;
    }

    public bool LerpFinished => 1 <= LerpFloat;
}

[Serializable]
public struct PlayerStatSet
{
    [SerializeField] public StatPoints health;
    [SerializeField] public int strength;
    [SerializeField] public int defence;
    [SerializeField] public int magicDefense;
    [SerializeField] public int maxMagic;
}

[Serializable]
public struct StatPoints
{
    [SerializeField]
    public int Current;
    public int Max;

    // optional; public float Modifier { get; private set; }, you'll need to adjust how Max works with a modifier.

    public float Percentage => Current / Max;

    public void AdjustCurrent(float amount) => Current = (int)Mathf.Clamp(Current + amount, 0f, Max);

    public void SetMax(float max) { Max = (int)max; Current = (int)Mathf.Min(Current, max); }
}






