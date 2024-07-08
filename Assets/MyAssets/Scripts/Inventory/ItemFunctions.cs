using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemFunctions : MonoBehaviour
{
    public static Dictionary<ItemFunction, UnityAction<int>> functionDictionary = new Dictionary<ItemFunction, UnityAction<int>>();
    [SerializeField]  StaticReferencesStorage staticReferencesStorageSO;
    


    ItemFunctions()
    {
        functionDictionary[ItemFunction.Heal] = Heal;
        functionDictionary[ItemFunction.Light] = Light;
        functionDictionary[ItemFunction.Explode] = Explode;

        Debug.Log("items functions set");
    }

    void Heal(int amount)
    {
        Debug.Log("Healed " + amount);

        staticReferencesStorageSO.playerStatsReference.RestoreHealth(amount);
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
