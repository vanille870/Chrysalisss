using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class ResetScriptableObjectOnQuit : Editor
{
    [SerializeField] StatsStorage[] statsStorage;

    // Start is called before the first frame update
    static ResetScriptableObjectOnQuit()
    {
        //EditorApplication.quitting += 
    }



}
