using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sword", menuName = "ScriptableObjects/Sword", order = 1)]
public class Sword : ScriptableObject
{
    public string Name = "sword";
    public int damage;
    public float chargeDamage;
}








