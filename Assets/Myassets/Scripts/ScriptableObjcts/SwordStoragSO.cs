using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SwordStorage", order = 1)]
public class SwordStorage : ScriptableObject
{
    [SerializeField] public List<Sword> swordList = new List<Sword>();

    public Dictionary<string, Sword> SwordDic = new Dictionary<string, Sword>();

    public void InitializeEquipmentSoDictionary()
    {
        foreach (Sword sword in swordList)
        {
            SwordDic.Add(sword.name, sword);
        }

        if (SwordDic.TryGetValue("Crystal_Sword", out PlayerEquiment.CurrentSwordOfPlayer) == false)
        {
            Debug.LogWarning("Sword not found");
        }
        
    }

}
