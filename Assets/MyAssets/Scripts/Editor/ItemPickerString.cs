#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;


[CustomPropertyDrawer(typeof(ItemFunctionPicker))]
public class ItemPickerINT : PropertyDrawer
{
    [SerializeField] string currentItemFunction;
    int ItemFunctionID;
    Vector3 currenVector3;


    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (property.propertyType != SerializedPropertyType.Integer)
        {
            EditorGUI.LabelField(position, label.text, "Use ItemPickerString only with strings in an item SO");
            return;
        }

        EditorGUILayout.TextField(currentItemFunction);

        if (GUILayout.Button("Heal"))
        {
            currentItemFunction = "Heal";
            property.intValue = 0;
        }

        else if (GUILayout.Button("Light"))
        {
            currentItemFunction = "Light";
            property.intValue = 1;
        }


    }
}
#endif