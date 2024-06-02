using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BaseStats", menuName = "ScriptableObjects/Stats/BaseStats", order = 1)]
public class BaseStatsStorage : ScriptableObject
{
    [SerializeField] public int baseMaxHealth;
    [SerializeField] public int baseStrength;
    [SerializeField] public int baseDefence;
    [SerializeField] public int baseMagicDefense;
    [SerializeField] public int baseMaxMagic;
}
