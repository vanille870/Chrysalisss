using UnityEngine;
using UnityEngine.UIElements;

public class GrassSpawner : MonoBehaviour, IBreakable
{
    public Material grassMaterial;
    public Mesh grassMesh;
    public int numInstances = 10;
    Vector3 startingPosition;
    Vector3[] positionStorage;
    public Matrix4x4[] MatrixArray;
    public Vector3 grassRotation;
    Vector3 Scaling;
    RaycastHit raycastHit;

    public bool SpawnGrassButton;

    public float minimumScaleOffset;
    public float maximumScaleOffset;

    public float minimumRotationOffset;
    public float maximumRotationOffset;

    public int seed = 0;

    struct GrassInstanceMatrix
    {
        public Matrix4x4 objectToWorld;

    };




    void Awake()
    {
        Random.InitState(seed);
        grassRotation = transform.eulerAngles;
        Scaling = transform.localScale;
        startingPosition = this.transform.position;
        MatrixArray = new Matrix4x4[numInstances];
        positionStorage = new Vector3[numInstances];
        transform.hasChanged = false;


        for (int i = 0; i < numInstances; ++i)
        {
            Vector3 Originpoint = new Vector3(startingPosition.x + Random.insideUnitCircle.x, startingPosition.y, startingPosition.z + Random.insideUnitCircle.y);
            positionStorage[i] = Originpoint;
            Physics.Raycast(Originpoint, Vector3.down, out raycastHit, Mathf.Infinity);
            Scaling = new Vector3(Scaling.x + Random.Range(minimumScaleOffset, maximumScaleOffset), Scaling.y + Random.Range(minimumScaleOffset, maximumScaleOffset), Scaling.z + Random.Range(minimumScaleOffset, maximumScaleOffset));
            grassRotation = new Vector3(0, grassRotation.y + Random.Range(minimumRotationOffset, maximumRotationOffset), grassRotation.z + Random.Range(minimumRotationOffset, maximumRotationOffset) / 10);

            MatrixArray[i] = Matrix4x4.TRS(raycastHit.point, Quaternion.Euler(0, grassRotation.y, grassRotation.z), Scaling);
        }

    }

    void Update()
    {
        InstanceGrass();
        print(transform.hasChanged);

        if (SpawnGrassButton)
        {
            seed += 1;
            CalculateAgain();
        }

        if (transform.hasChanged == true)
        {
            ChangeLocation();
            transform.hasChanged = false;
        }

    }

    void CalculateAgain()
    {
        Random.InitState(seed);
        grassRotation = transform.eulerAngles;
        Scaling = transform.localScale;
        startingPosition = transform.position;
        MatrixArray = new Matrix4x4[numInstances];
        positionStorage = new Vector3[numInstances];

        
        for (int i = 0; i < numInstances; ++i)
        {
            Vector3 Originpoint = new Vector3(startingPosition.x + Random.insideUnitCircle.x, startingPosition.y, startingPosition.z + Random.insideUnitCircle.y);
            positionStorage[i] = Originpoint;
            Physics.Raycast(Originpoint, Vector3.down, out raycastHit, Mathf.Infinity);
            Scaling = new Vector3(Scaling.x + Random.Range(minimumScaleOffset, maximumScaleOffset), Scaling.y + Random.Range(minimumScaleOffset, maximumScaleOffset), Scaling.z + Random.Range(minimumScaleOffset, maximumScaleOffset));
            grassRotation = new Vector3(0, grassRotation.y + Random.Range(minimumRotationOffset, maximumRotationOffset), grassRotation.z + Random.Range(minimumRotationOffset, maximumRotationOffset) / 10);

            MatrixArray[i] = Matrix4x4.TRS(raycastHit.point, Quaternion.Euler(0, grassRotation.y, grassRotation.z), Scaling);
        }


        SpawnGrassButton = false;
        print("grass");
    }

    void InstanceGrass()
    {
        RenderParams rp = new RenderParams(grassMaterial);
        Graphics.RenderMeshInstanced(rp, grassMesh, 0, MatrixArray); //final argument can take an array
    }

    void ChangeLocation()
    {
       CalculateAgain();
    }

    public void Damage(float damage, Vector3 position)
    {
        damage = 5;
        position = Vector3.one;
    }
}