using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpawnByLocation;

[CreateAssetMenu(fileName = "SpawnPoints", menuName = "ScriptableObjects/Locations/SpawnPoints", order = 1)]
public class SpawnPoints : ScriptableObject
{
    [SerializeField] GameObject go;

    
    public List<Spawns> spawnPointColections = new List<Spawns>();
}
