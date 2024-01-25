using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bakeMeshArchived : MonoBehaviour
{

    public Mesh mirageMesh;
    public SkinnedMeshRenderer TargetMesh;
    public GameObject MeshGO = default;

    // Start is called before the first frame update
    void Start()
    {
        TargetMesh.BakeMesh(mirageMesh, false);
        GetComponent<MeshFilter>().mesh = mirageMesh;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
