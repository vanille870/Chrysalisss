using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] StatsStorage StatsStorageSO;

    public PlayerStatSet playerStatSet;

    public int playerHealth;
    public HealthNumberManager damageNumberManager;
    public Transform damageNumberPos;

    // Start is called before the first frame update
    void Awake()
    {
        playerStatSet = StatsStorageSO.BaseStats;
        HealthBar.SetUpHealthBar(playerStatSet.health.Max, playerStatSet.health.Current);
    }

    public void RecieveDamage(int damage)
    {
        damageNumberManager.InstantiateHealthNumber(damage, damageNumberPos.position, TypeOfHealthNumber.playerDamage);
        playerStatSet.health.Current -= Mathf.Clamp(damage, 1, 9999);
        HealthBar.UpdateHealthBar(damage, true);
        //playerHealthSlider.value = playerHealth;
    }

    public void RestoreHealth(int amount)
    {
        playerStatSet.health.AdjustCurrent(amount);
        damageNumberManager.InstantiateHealthNumber(amount, damageNumberPos.position, TypeOfHealthNumber.heal);
        HealthBar.UpdateHealthBar(amount, false);
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
