using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInventory : MonoBehaviour
{
    public static List<InventoryItemSlot> playerInventory = new List<InventoryItemSlot>();
    public GameObject inventoryParentObject;
    public static GameObject inventoryParentObjectStatic;
    int maximumInventorySpace = 10;

    [SerializeField] InventoryItemsData Test;
    [SerializeField] InventoryItemsData Test2;

    [SerializeField] bool ReOrderButton;


    [Header("debug")]
    Sprite potionS;
    Sprite torchs;

    private int currentID = 0;
    private static int i;

    // Start is called before the first frame update
    void Start()
    {
        inventoryParentObjectStatic = inventoryParentObject;

        foreach (Transform invGOTransform in inventoryParentObject.transform)
        {
            InventoryItemSlot itemSlot = new InventoryItemSlot(invGOTransform.gameObject);
            itemSlot.ReadyGO();

            playerInventory.Add(itemSlot);
        }

        ReAssignIndex();


        AddItemToInventoryIfRoom(Test);
        AddItemToInventoryIfRoom(Test2);

        AddItemToInventoryIfRoom(Test);
        AddItemToInventoryIfRoom(Test2);

        AddItemToInventoryIfRoom(Test);
        AddItemToInventoryIfRoom(Test2);

        AddItemToInventoryIfRoom(Test);
        AddItemToInventoryIfRoom(Test2);
    }

    public bool AddItemToInventoryIfRoom(InventoryItemsData inventoryItemData)
    {
        foreach (InventoryItemSlot inventoryItemSlot in playerInventory)
        {
            if (inventoryItemSlot.hasItem == false)
            {
                inventoryItemSlot.hasItem = true;
                inventoryItemSlot.inventoryItemsDataSO = inventoryItemData;
                inventoryItemSlot.UpdateGO();
                return true;
            }
        }

        print("inventory full");
        return false;

    }

    public static void ReorderItems()
    {
        foreach (InventoryItemSlot inventoryItemSlot in playerInventory)
        {
            if (inventoryItemSlot.hasItem == false)
            {
                inventoryItemSlot.slotGO.transform.SetAsLastSibling();
            }
        }

        ReAssignIndex();
    }

    public static void ReAssignIndex()
    {
        i = -1;

        foreach (Transform gameObjectTrans in inventoryParentObjectStatic.transform)
        {
            i++;
            gameObjectTrans.gameObject.GetComponent<ButtonListeners>().index = i;
        }
    }
}
