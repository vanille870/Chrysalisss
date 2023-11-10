using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TerrainUtils;
using UnityEngine.TerrainTools;

public class Terrain_grass : MonoBehaviour
{

    public Terrain terrain;
    public int[,] grassInt;


    // Start is called before the first frame update
    void Start()
    {
        grassInt = terrain.terrainData.GetDetailLayer(0,0, terrain.terrainData.detailWidth, terrain.terrainData.detailHeight, 0);
        print(grassInt);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
