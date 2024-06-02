using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine.Events;


[Serializable]
public enum ItemFunctionType { Heal, Torch, Bomb };

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
        itemImage.enabled = true;
        itemImage.sprite = inventoryItemsDataSO.itemImage;
        GOtext.text = inventoryItemsDataSO.itemName;
        itemImage.enabled = true;

        GObutton.onClick.AddListener(() => inventoryItemsDataSO.itemFunction(inventoryItemsDataSO.effectMagintude));
        GObutton.onClick.AddListener(DisableGO);
        slotGO.SetActive(true);
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
        itemImage.enabled = false;
    }
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

public class PostitionToVector3 : PropertyAttribute
{
    public GameObject spawnGO;
    public Vector3 vector3;
}





