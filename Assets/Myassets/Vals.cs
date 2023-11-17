using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
[ExecuteInEditMode]
public class CuttableMass : MonoBehaviour
{
    private struct InstanceData
    {
        public Matrix4x4 objectToWorld;
    };

    [Header("Visuals")]
    [SerializeField]
    private Mesh mesh = null;

    [SerializeField]
    private Material material = null;

    [Header("Properties")]
    [SerializeField]
    private LayerMask layerMaskToAffect;

    [SerializeField, Range(0, 1024)]
    private int amount = 1;

    [SerializeField, Range( 0.1f, 100f )]
    private float range = 1f;

    [Space, SerializeField]
    private int RandomSeed = 0;

    private RenderParams rp;
    private MaterialPropertyBlock propBlock;
    private InstanceData[] instances;
    private float[] cutData;

    public void Cut(Vector3 position, float range)
    {
        float rangeSqr = range*range;
        for( int i = 0; i < amount; ++i )
        {
            Matrix4x4 instance = instances[i].objectToWorld;
            Vector3 grassPos = new Vector3(instance[0,3], instance[1,3], instance[2,3]);

            Vector3 diff = (position-grassPos);

            if( diff.sqrMagnitude < rangeSqr )
            {
                cutData[i] *= 0.6f;
            }
        }

        propBlock.SetFloatArray( "_CutAmount", cutData );
    }

    private void Awake()
    {
        rp = new RenderParams(material);
        propBlock = new MaterialPropertyBlock();
        rp.matProps = propBlock;

        instances = instances ?? new InstanceData[amount];
        cutData = cutData ?? new float[amount];

        Spawn();
    }

    private void Update()
    {
        Graphics.RenderMeshInstanced(rp, mesh, 0, instances);
    }

    private void Spawn()
    {
        Random.InitState(RandomSeed);

        Vector3 iPos = this.transform.position;
    
        for( int i = 0; i < amount; ++i )
        {
            Vector2 offset = Random.insideUnitCircle * range;
            Vector3 newOffset = new Vector3( iPos.x + offset.x, iPos.y + range, iPos.z + offset.y );
            Physics.Raycast( newOffset, Vector3.down, out var hitInfo, range * 2f, layerMaskToAffect ); // Don't care if it doesn't hit anything, we'll just assume it's floating for now.

            instances[i] = new InstanceData() {
                objectToWorld = Matrix4x4.TRS( hitInfo.point, Quaternion.identity, Vector3.one ),
            };
            cutData[i] = 1f; 
        }

        propBlock.SetFloatArray( "_CutAmount", cutData );
    }

    private void OnValidate()
    {
        this.enabled = !( material == null || mesh == null );

        propBlock = propBlock ?? new MaterialPropertyBlock();

        rp = new RenderParams(material);
        rp.matProps = propBlock;

        if( this.TryGetComponent<SphereCollider>(out var collider) )
        {
            if( Mathf.Abs(collider.radius - range) > 0.001f )
            {
                collider.radius = range;
                Spawn();
            }
        }

        if( (instances?.Length ?? -1) != amount )
        {
            instances = new InstanceData[amount];
            cutData = new float[amount];
            Spawn();
        }
    }

    private void OnDrawGizmosSelected()
    {
        if( UnityEditor.Selection.activeGameObject == this.gameObject )
        {
            Spawn();
        }
    }
}