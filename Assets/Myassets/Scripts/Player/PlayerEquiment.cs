using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquiment : MonoBehaviour
{
    [SerializeField] SwordStorage swordStorageSO;

    public Sword CurrentSwordOfPlayer;

    void Start()
    {
        swordStorageSO.InitializeEquipmentSoDictionary();
        //print(swordStorageSO.SwordID["Crystal_Sword"]);
    }



}
