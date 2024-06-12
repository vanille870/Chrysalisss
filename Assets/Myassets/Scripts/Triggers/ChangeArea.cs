using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SpawnByLocation;
using UnityEditor;
using Unity.Mathematics;

public class ChangeArea : MonoBehaviour
{
    [SerializeField] GameObject PlayerPrefab;
    [SerializeField] GameObject LoadingScreen;
    [SerializeField] CustomGameLoop customGameLoopScript;
    [SerializeField] SpawnPoints spawnPointsSO;

    [SerializeField] string SceneToLoad;
    [SerializeField] int spawnNumber;

    [SerializeField] bool isInitScene;


    private GameObject PlayerGO;


    [System.Serializable]
    public struct Timer
    {
        public float Duration;
        private float Clock;

        public Timer(float duration, float time = 0f)
        {
            Duration = duration;
            Clock = time;
        }

        public void SetClock()
        {
            Clock = Time.unscaledTime + Duration;
        }

        public bool IsFinished => Time.unscaledTime >= Clock;
    }

    [SerializeField]
    Timer SceneLoad = new Timer();

    public void OnTriggerEnter()
    {
        ChangeAreaFunction();
    }

    public void OnSceneLoad(Scene scene, LoadSceneMode loadSceneMode)
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(SceneToLoad));
        SceneManager.sceneLoaded -= OnSceneLoad;

        customGameLoopScript.EnableLoop();
        FindSpawnPointAndSpawnlayer(spawnNumber);
    }

    public void FindSpawnPointAndSpawnlayer(int InputSpawnNumber)
    {
        foreach (Spawns spawn in spawnPointsSO.spawnPointColections)
        {
            if (InputSpawnNumber == spawn.ID)
            {
                PlayerGO = Instantiate(PlayerPrefab, spawn.Location, quaternion.identity);
                PlayerGO.GetComponent<InitReferencesToMaster>().InitPlayer();
                return;
            }
        }

        Debug.LogError("Warning: no spawn found");
    }

    public void ChangeAreaFunction()
    {
        print("loading");


        if (isInitScene == false)
        {
            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
            LoadingScreen.SetActive(true);
        }

        SceneManager.sceneLoaded += OnSceneLoad;
        customGameLoopScript.DisableLoop();
        SceneManager.LoadSceneAsync(SceneToLoad, LoadSceneMode.Additive);
    }
}
