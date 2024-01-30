using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] BaseStatsStorage baseStatsStorageSO;

    public int maxHealth { get; private set; }
    public int Strength { get; private set; }
    public int defence { get; private set; }
    public int magicDefense { get; private set; }
    public int MaxMagic { get; private set; }

    public void SetStatsFromBase()
    {
        maxHealth = baseStatsStorageSO.baseMaxHealth;
        Strength = baseStatsStorageSO.baseStrength;
        defence = baseStatsStorageSO.baseDefence;
        magicDefense = baseStatsStorageSO.baseMagicDefense;
        MaxMagic = baseStatsStorageSO.baseMaxMagic;
    }
}
