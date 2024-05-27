using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "InventoryItemData", menuName = "ScriptableObjects/Items/InventoryItem", order = 1)]
public class InventoryItemsData : ScriptableObject
{
    public string itemName;
    public int itemID;
    public int effectMagintude;
    public Sprite itemImage;
    public ItemFunctionType itemFunctionType;
    public UnityAction<int> itemFunction;

    public InventoryItemsData()
    {
        itemFunction = ItemFunctions.functionDictionary[itemFunctionType];
    }
}

public static class ItemFunctions
{
    public static Dictionary<ItemFunctionType, UnityAction<int>> functionDictionary = new Dictionary<ItemFunctionType, UnityAction<int>>();


    static ItemFunctions()
    {
        functionDictionary[ItemFunctionType.Heal] = Heal;
        functionDictionary[ItemFunctionType.Torch] = Light;
        functionDictionary[ItemFunctionType.Bomb] = Explode;
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


