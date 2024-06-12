using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquiment : MonoBehaviour
{
    [SerializeField] SwordStorage swordStorageSO;

    public static Sword CurrentSwordOfPlayer;

    bool HasRunned;

    void Start()
    {
        /*if (HasRunned == false)
        {
            swordStorageSO.InitializeEquipmentSoDictionary();
            //print(swordStorageSO.SwordID["Crystal_Sword"]);
            HasRunned = true;
        }*/
    }



}
