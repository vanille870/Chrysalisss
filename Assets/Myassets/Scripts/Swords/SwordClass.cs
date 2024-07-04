using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sword", menuName = "ScriptableObjects/Sword", order = 1)]
public class Sword : ScriptableObject
{
    public string Name = "Crystal_Sword";
    public SwordAttackTypes swordAttackTypes;
}

[Serializable]
public struct SwordAttackTypes
{
    public NormalAttack normalAttack;
    public ChargedAttack chargedAttack;
    public FailedAttack failedAttack;
}

[Serializable]
public struct NormalAttack
{
    public int damage;
    public int staggerDamage;
}

[Serializable]
public struct ChargedAttack
{
    public int damage;
    public int staggerDamage;
}

[Serializable]
public struct FailedAttack
{
    public int damage;
    public int staggerDamage;
}









