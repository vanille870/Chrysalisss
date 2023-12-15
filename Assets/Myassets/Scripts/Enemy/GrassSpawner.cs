using UnityEngine;
using static UnityEngine.ParticleSystem;


public class GrassSpawner : MonoBehaviour, IBreakable
{
    public Material grassMaterial;
    public Mesh grassMesh;
    public int numInstances = 10;
    public Matrix4x4[] MatrixArray;
    public Vector3 grassRotation;
    Vector3 Scaling;
    RaycastHit raycastHit;

    public bool SpawnGrassButton;

    public float minimumScaleOffset;
    public float maximumScaleOffset;

    public float minimumRotationOffset;
    public float maximumRotationOffset;

    public float cutRange = 1f;

    public int seed = 0;

    MaterialPropertyBlock propertyBlock;
    float[] cutData;
    RenderParams renderP;
    Vector3 startingPosition;
    Vector3[] positionStorage;
    public ParticleSystem grassBladesPS;
    GameObject particleSytemObject;
    public EmitParams emitP;
    public GameObject grassParticleSys;
    public Vector3 particlSysLocation;
    public ParticleSystemPoolManager particleSystemPoolManager;
    bool hasParticleSys = false;

    void Awake()
    {

        //initialize variables
        emitP.applyShapeToPosition = true;
        particleSystemPoolManager = GameObject.Find("ParticleSysManager").GetComponent<ParticleSystemPoolManager>();
        Random.InitState(seed);

        grassRotation = transform.eulerAngles;
        Scaling = transform.localScale;
        startingPosition = transform.position;

        MatrixArray = new Matrix4x4[numInstances];
        positionStorage = new Vector3[numInstances];
        cutData = new float[numInstances];
        transform.hasChanged = false;
        
        propertyBlock = new MaterialPropertyBlock();
        renderP = new RenderParams(grassMaterial);
        renderP.matProps = propertyBlock;

        getParticlSystemPosition();


        for (int i = 0; i < numInstances; ++i)
        {
            Vector3 Originpoint = new Vector3(startingPosition.x + Random.insideUnitCircle.x, startingPosition.y, startingPosition.z + Random.insideUnitCircle.y);
            Physics.Raycast(Originpoint, Vector3.down, out raycastHit, Mathf.Infinity);
            Scaling = new Vector3(Scaling.x + Random.Range(minimumScaleOffset, maximumScaleOffset), Scaling.y + Random.Range(minimumScaleOffset, maximumScaleOffset), Scaling.z + Random.Range(minimumScaleOffset, maximumScaleOffset));
            grassRotation = new Vector3(0, grassRotation.y + Random.Range(minimumRotationOffset, maximumRotationOffset), grassRotation.z + Random.Range(minimumRotationOffset, maximumRotationOffset) / 10);
            MatrixArray[i] = Matrix4x4.TRS(raycastHit.point, Quaternion.Euler(0, grassRotation.y, grassRotation.z), Scaling);
            cutData[i] = 0;
        }
    }

    void Update()
    {
        renderP = new RenderParams(grassMaterial);
        renderP.matProps = propertyBlock;
        InstanceGrass();


        if (SpawnGrassButton)
        {
            seed += 1;
            CalculateGrassAgain();
            SpawnGrassButton = false;
        }

        if (transform.hasChanged == true)
        {
            CalculateGrassAgain();
            transform.hasChanged = false;
        }

    }

    void InstanceGrass()
    {
        renderP.matProps = propertyBlock;
        Graphics.RenderMeshInstanced(renderP, grassMesh, 0, MatrixArray); //final argument can take an array
    }

    void CalculateGrassAgain()
    {
        Random.InitState(seed);

        grassRotation = transform.eulerAngles;
        Scaling = transform.localScale;
        startingPosition = transform.position;

        MatrixArray = new Matrix4x4[numInstances];
        positionStorage = new Vector3[numInstances];

        //array to pass into GPU isnatncing function so the game knows what to cut
        cutData = new float[numInstances];

        transform.hasChanged = false;
        renderP = new RenderParams(grassMaterial);
        renderP.matProps = propertyBlock;


        for (int i = 0; i < numInstances; ++i)
        {
            //Determine random location and fire raycast down for location on ground
            Vector3 Originpoint = new Vector3(startingPosition.x + Random.insideUnitCircle.x, startingPosition.y, startingPosition.z + Random.insideUnitCircle.y);
            Physics.Raycast(Originpoint, Vector3.down, out raycastHit, Mathf.Infinity);
            Scaling = new Vector3(Scaling.x + Random.Range(minimumScaleOffset, maximumScaleOffset), Scaling.y + Random.Range(minimumScaleOffset, maximumScaleOffset), Scaling.z + Random.Range(minimumScaleOffset, maximumScaleOffset));
            grassRotation = new Vector3(0, grassRotation.y + Random.Range(minimumRotationOffset, maximumRotationOffset), grassRotation.z + Random.Range(minimumRotationOffset, maximumRotationOffset) / 10);

            MatrixArray[i] = Matrix4x4.TRS(raycastHit.point, Quaternion.Euler(0, grassRotation.y, grassRotation.z), Scaling);
        }

    }

    public void Damage(float damage, float range, Vector3 position)
    {
        float rangeSqr = range * range;

        if (hasParticleSys == false)
        {
            AssignParticlSys();
            hasParticleSys = true;
        }


        for (int i = 0; i < numInstances; ++i)
        {
            Matrix4x4 instance = MatrixArray[i];
            Vector3 grassPos = new Vector3(instance[0, 3], instance[1, 3], instance[2, 3]);

            Vector3 diff = (position - grassPos);

            if (diff.sqrMagnitude < rangeSqr && cutData[i] == 0)
            {
                cutData[i] = 1f;
                emitP.position = grassPos;
                grassBladesPS.Emit(emitP, 20);
            }
        }
        propertyBlock.SetFloatArray("_GrassIsCut", cutData); //set material block so shader knows what to 'cut'
    }

    public void getParticlSystemPosition()
    {
        Physics.Raycast(this.transform.position, Vector3.down, out raycastHit, Mathf.Infinity);
        particlSysLocation = raycastHit.point;
    }

    public void AssignParticlSys()
    {
        particleSystemPoolManager.GetParticleSystem(particlSysLocation, gameObject);
    }
}