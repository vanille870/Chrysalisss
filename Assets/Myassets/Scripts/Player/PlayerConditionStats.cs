using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerConditionStats : MonoBehaviour
{
    [SerializeField] PlayerStats playerStats;

    public int playerHealth;
    public int maxPlayerHealth;
    public Slider playerHealthSlider;

    // Start is called before the first frame update
    void Awake()
    {
        /*playerStats.SetStatsFromBase();

        maxPlayerHealth = playerStats.maxHealth;
        playerHealthSlider.maxValue = maxPlayerHealth;
        playerHealthSlider.value = maxPlayerHealth;
        playerHealthSlider.minValue = 0;*/
    }

    public void RecieveDamage(int damage)
    {
        int recievedDamage = damage;
       
        playerHealth -= Mathf.Clamp(recievedDamage, 1, 9999);
        //playerHealthSlider.value = playerHealth;

    }
}
