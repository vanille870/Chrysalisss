using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sword", menuName = "ScriptableObjects/Sword", order = 1)]
public class Sword : ScriptableObject
{
    public string Name = "Crystal_Sword";
    public int StaggerDamage;
    public int ChargeStaggerDamage;
    public int damage;
    public int chargeDamage;
}








