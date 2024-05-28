using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaChange : MonoBehaviour
{
    [SerializeField] int sceneIndex;
    [SerializeField] int currentSceneIndex;


    // Start is called before the first frame update
    void OnTriggerEnter()
    {
        SceneManager.UnloadSceneAsync(currentSceneIndex);
        SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive);
    }
}
