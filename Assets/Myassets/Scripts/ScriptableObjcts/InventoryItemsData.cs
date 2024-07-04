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

public class ItemFunctions : MonoBehaviour
{
    public static Dictionary<ItemFunction, UnityAction<int>> functionDictionary = new Dictionary<ItemFunction, UnityAction<int>>();


    static ItemFunctions()
    {
        functionDictionary[ItemFunction.Heal] = Heal;
        functionDictionary[ItemFunction.Light] = Light;
        functionDictionary[ItemFunction.Explode] = Explode;

        Debug.Log("items functions set");
    }

    static void Heal(int amount)
    {
        Debug.Log("Healed " + amount);
    }

    static void Light(int amount)
    {
        Debug.Log("Light " + amount);
    }

    static void Explode(int radius)
    {
        Debug.Log("BOOM " + radius);
    }
}



