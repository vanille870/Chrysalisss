using UnityEngine;

public class GrassSpawner : MonoBehaviour
{
    public Material grassMaterial;
    public Mesh grassMesh;
    public int numInstances = 10;
    Vector3 startingPosition;

    void Start()
    {
      startingPosition = this.transform.position;
    }

    void Update()
    {
        startingPosition = this.transform.position;
        RenderParams rp = new RenderParams(grassMaterial);
        Matrix4x4[] instData = new Matrix4x4[numInstances];

        for (int i = 0; i < numInstances; ++i)
        {
            instData[i] = Matrix4x4.Translate(new Vector3(startingPosition.x + i, startingPosition.y, startingPosition.z));
            Graphics.RenderMeshInstanced(rp, grassMesh, 0, instData);
            print("spawnd");
        }


    }
}