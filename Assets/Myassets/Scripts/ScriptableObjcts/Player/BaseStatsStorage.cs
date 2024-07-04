using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "BaseStats", menuName = "ScriptableObjects/Stats/BaseStats", order = 1)]
public class StatsStorage : ScriptableObject
{
    [SerializeField]
    public PlayerStatSet BaseStats;
    [SerializeField]
    public PlayerStatSet CurrentStatsTransfer;

    public PlayerStats playerStatsScript;

    [NonSerialized]
    private PlayerStatSet DefaultStatSet;

    #if UNITY_EDITOR
    void OnDisable()
    {
        Debug.Log("Disabled");
        CurrentStatsTransfer = DefaultStatSet;
    }

    void OnEnable()
    {
         Debug.Log("Ensabled");
        DefaultStatSet = CurrentStatsTransfer;
    }
    #endif
}
