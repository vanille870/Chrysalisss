using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SwordStorage", order = 1)]
public class SwordStorage : ScriptableObject
{
    [SerializeField] public List<Sword> swordList = new List<Sword>();

    public Dictionary<string, Sword> SwordDick = new Dictionary<string, Sword>();

}
