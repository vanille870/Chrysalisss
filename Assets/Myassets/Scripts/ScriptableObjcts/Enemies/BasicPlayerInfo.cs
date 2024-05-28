using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "BasicPlayerInfo", menuName = "ScriptableObjects/Player/BasicInfo", order = 1)]
public class BasicPlayerInfo : ScriptableObject
{
    public CharacterController playerController;
    public GameObject playerGO;
}