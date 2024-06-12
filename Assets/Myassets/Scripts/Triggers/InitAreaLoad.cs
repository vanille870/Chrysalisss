using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SpawnByLocation;
using UnityEditor;
using Unity.Mathematics;
using UnityEngine.InputSystem;

public class InitAreaLoad : MonoBehaviour
{
    [SerializeField] GameObject PlayerPrefab;
    [SerializeField] SpawnPoints spawnPointsSO;

    [SerializeField] string sceneToLoad;
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

    public void Start()
    {
        ChangeAreaFunction();
    }

    public void OnSceneLoad(Scene scene, LoadSceneMode loadSceneMode)
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneToLoad));
        FindSpawnPointAndSpawnlayer(spawnNumber);
        InputManager.EnableControls();
        SceneManager.sceneLoaded -= OnSceneLoad;
        Destroy(gameObject);
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
        SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);
        SceneManager.sceneLoaded += OnSceneLoad;
    }
}
