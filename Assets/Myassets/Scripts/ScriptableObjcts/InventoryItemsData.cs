using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum ItemFunction { Heal, Light, Explode }

[CreateAssetMenu(fileName = "InventoryItemData", menuName = "ScriptableObjects/Items/InventoryItem", order = 1)]
public class InventoryItemsData : ScriptableObject
{
    public string itemName;

    public int itemID;
    public int effectMagintude;
    public Sprite itemImage;
    public UnityAction<int> itemFunction;
    private bool isSetUp = false;
    public ItemFunction itemFunctionEnum;

    public void InventoryItemsDataSetup()
    {
        Debug.Log("setup");
        itemFunction = ItemFunctions.functionDictionary[itemFunctionEnum];
        isSetUp = true;

    }
}





