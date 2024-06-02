using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SpawnByLocation;

public class ChangeArea : MonoBehaviour
{
    [SerializeField] GameObject LoadingScreen;
    [SerializeField] GameObject Player;
    [SerializeField] CustomGameLoop customGameLoopScript;
    [SerializeField] SpawnPoints spawnPointsSO;

    [SerializeField] string SceneToLoad;
    [SerializeField] int spawnNumber;


    [SerializeField] bool isInitScene;



    public void OnTriggerEnter()
    {
        LoadingScreen.SetActive(true);
        GameMaster.gameMasterSingleton.playerController.enabled = false;

        if (isInitScene == false)
        {
            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        }

        SceneManager.sceneLoaded += OnSceneLoad;
        print("loading");
        SceneManager.LoadSceneAsync(SceneToLoad, LoadSceneMode.Additive);
        customGameLoopScript.DisableLoop();

        Destroy(gameObject);
    }

    public void OnSceneLoad(Scene scene, LoadSceneMode loadSceneMode)
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(SceneToLoad));
        SceneManager.sceneLoaded -= OnSceneLoad;

        LoadingScreen.SetActive(false);

        customGameLoopScript.EnableLoop();
        FindSpawnPointAndMovePlayer(spawnNumber);

        GameMaster.gameMasterSingleton.playerController.enabled = true;
    }

    public void FindSpawnPointAndMovePlayer(int InputSpawnNumber)
    {
        foreach (Spawns spawn in spawnPointsSO.spawnPointColections)
        {
            if (spawnNumber == spawn.ID)
            {
                GameMaster.gameMasterSingleton.MovePlayer(spawn.Location);
                return;
            }
        }

        Debug.LogError("Warning: no spawn found");
    }

    


}
