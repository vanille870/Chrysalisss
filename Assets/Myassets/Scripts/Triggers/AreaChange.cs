using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaChange : MonoBehaviour
{
    [SerializeField] string sceneName;
    [SerializeField] string currentScene;


    // Start is called before the first frame update
    void OnTriggerEnter()
    {
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
    }
}
