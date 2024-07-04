using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] StatsStorage StatsStorageSO;

    public PlayerStatSet playerStatSet;

    public int playerHealth;
    public DamageNumberManager damageNumberManager;
    public Transform damageNumberPos;

    // Start is called before the first frame update
    void Awake()
    {
        playerStatSet = StatsStorageSO.BaseStats;
        HealthBar.SetUpHealthBar(playerStatSet.health.Max, playerStatSet.health.Current);
    }

    public void RecieveDamage(int damage)
    {
        damageNumberManager.InstantiateDamageNumber(damage, damageNumberPos.position, EntityType.player, false, true, true, true);
        playerStatSet.health.Current -= Mathf.Clamp(damage, 1, 9999);
        HealthBar.UpdateHealthBar(damage);
        //playerHealthSlider.value = playerHealth;
    }

    public void SetStatsFromBase()
    {

    }

    public void SaveStatsToSO()
    {
        StatsStorageSO.CurrentStatsTransfer = playerStatSet;
    }

    public void LoadStatsFromSO()
    {
        print("stats loaded");
        playerStatSet = StatsStorageSO.CurrentStatsTransfer;

        HealthBar.SetUpHealthBar(playerStatSet.health.Max, playerStatSet.health.Current);
    }

}
