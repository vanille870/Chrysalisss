using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerSpawner : MonoBehaviour
{

    
    GameObject playerParent;

    [Serializable]
    public struct SpawnPointPerLocation
    {
        public  FieldSpawnPoints fieldSpawnPoints;
        public  TownSpawnPoints townSpawnPoints;
    }

    [Serializable]
    public struct FieldSpawnPoints
    {
        public  Vector3 fromTownPos;
        public  Vector3 fromInitPos;
    }

    [Serializable]
    public struct TownSpawnPoints
    {
        public  Vector3 fromFieldPos;
    }

    [SerializeField]
    SpawnPointPerLocation spawnPointPerLocation;

    // Start is called before the first frame update
    void SpawnPlayerToLocation(Vector3 inputPosition)
    {
        playerParent.transform.position = inputPosition;
    }
}
