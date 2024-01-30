using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquiment : MonoBehaviour
{
    [SerializeField] SwordStorage swordStorageSO;

    public static Sword CurrentSwordOfPlayer;

    void Start()
    {
        InitializeEquipmentSoDictionary();
        //print(swordStorageSO.SwordID["Crystal_Sword"]);
    }

    void InitializeEquipmentSoDictionary()
    {
        foreach (Sword sword in swordStorageSO.swordList)
        {
            swordStorageSO.SwordDick.Add(sword.name, sword);
        }

        CurrentSwordOfPlayer = swordStorageSO.SwordDick["Crystal_Sword"];
    }
    
}
