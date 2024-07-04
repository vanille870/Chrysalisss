#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;


[CustomPropertyDrawer(typeof(PostitionToVector3))]
public class SpawnToVector3 : PropertyDrawer
{
    [SerializeField] Object currentGameObject;
    Vector3 currenVector3;


    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (property.propertyType != SerializedPropertyType.Vector3)
        {
            EditorGUI.LabelField(position, label.text, "Use PostitionToVector3 only with Vector 3");
            return;
        }

        currentGameObject = EditorGUILayout.ObjectField(currentGameObject, typeof(GameObject), true);

        if (GUILayout.Button("Make new spawn"))
        {
            ConvertTransformToPos(property);
        }

        property.vector3Value = EditorGUI.Vector3Field(position, property.name, property.vector3Value);
    }

    void ConvertTransformToPos(SerializedProperty serializedProperty)
    {
        if (currentGameObject == null)
        {
            Debug.LogWarning("No GameObject selected");
            return;
        }

        serializedProperty.vector3Value = ((GameObject)currentGameObject).transform.position;
    }
}
#endif