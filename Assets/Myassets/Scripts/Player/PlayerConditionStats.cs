using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerConditionStats : MonoBehaviour
{

    public int playerHealth;
    public int maxPlayerHealth;
    public Slider playerHealthSlider;


    // Start is called before the first frame update
    void Start()
    {
        maxPlayerHealth = playerHealth;
        playerHealthSlider.maxValue = maxPlayerHealth;
        playerHealthSlider.minValue = 0;
        playerHealthSlider.value = maxPlayerHealth;
    }

    public void RecieveDamage(int damage)
    {
        playerHealth -= damage;
        playerHealthSlider.value = playerHealth;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
