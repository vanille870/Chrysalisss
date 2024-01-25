using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem : MonoBehaviour
{
    public static LevelSystem Instance { get; private set; }

    public int EXPStat = 0; 

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Instance is already set, two LevelSystem exist! Panic!!!");
            return;
        }
        Instance = this;
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    public void AddExp(int expGiven)
    {
        EXPStat += expGiven;
    }
}
