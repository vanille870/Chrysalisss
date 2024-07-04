#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Reflection.Emit;
using SpawnByLocation;


/*[CustomEditor(typeof(SpawnPoints))]
public class TransformToVector3 : Editor
{
    [SerializeField] Object currentGameObject;



    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        currentGameObject = EditorGUILayout.ObjectField(currentGameObject, typeof(GameObject), true);

        if (GUILayout.Button("Make new spawn"))
        {
            ConvertTransformToPos();
        }
    }

    void ConvertTransformToPos()
    {
        SpawnPoints spawnPointsScript = (SpawnPoints)target;
        Spawns currentSpawn = new Spawns(((GameObject)currentGameObject).transform.position);

        spawnPointsScript.spawnPointColections.Add(currentSpawn);
    }
}*/
#endif

