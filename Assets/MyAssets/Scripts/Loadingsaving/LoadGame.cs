
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class LoadGame : MonoBehaviour
{

    [SerializeField] GameObject confirmationScreen;
    [SerializeField] GameObject noFileWarning;
    [SerializeField] Button confirmOverwriteButton;

    void Awake()
    {
        SaveSlotsManager.confirmationScreen = confirmationScreen;
        SaveSlotsManager.noFileWarning = noFileWarning;
    }

    // Start is called before the first frame update
    public void NewGame(int InputSaveNumber)
    {
        SaveSlotsManager.CheckSaveSlotForSaving(InputSaveNumber);
    }

    public void LoadExistingGame(int InputSaveNumber)
    {
        SaveSlotsManager.CheckSaveSlotForLoading(InputSaveNumber);
    }

    public void SetSaveAndOvrwrite()
    {
        SaveSlotsManager.SetSaveSlotAndLoadGame(true);
    }
}

public static class SaveSlotsManager
{
    public static int saveSlotNumber;
    public static string currentPath;
    public static bool SaveSet;

    public static GameObject confirmationScreen;
    public static GameObject noFileWarning;

    public static void SetPath(int InputSaveSlotNumber)
    {
        saveSlotNumber = InputSaveSlotNumber;
        currentPath = Application.persistentDataPath + "/ChrysalisSaveData" + "/SaveSlot" + InputSaveSlotNumber + "/";
    }

    public static void CheckSaveSlotForSaving(int InputSaveSlotNumber)
    {
        SetPath(InputSaveSlotNumber);

        if (Directory.Exists(currentPath))
        {
            confirmationScreen.SetActive(true);
        }

        else
        {
            Directory.CreateDirectory(currentPath);
            SetSaveSlotAndLoadGame(false);
        }
    }

    public static void CheckSaveSlotForLoading(int InputSaveSlotNumber)
    {
        SetPath(InputSaveSlotNumber);

        if (Directory.Exists(currentPath) == false)
        {
            noFileWarning.SetActive(true);
            Debug.Log("No file detected");
        }

        else
        {
            Directory.CreateDirectory(currentPath);
            SetSaveSlotAndLoadGame(false);
        }
    }

    public static void DeleteSaveSlot(int InputSaveSlotNumber)
    {
        SetPath(InputSaveSlotNumber);
        Directory.Delete(currentPath, true);
    }

    public static void SetSaveSlotAndLoadGame(bool OverWriteGame)
    {
        if (OverWriteGame == true)
        {
            DeleteSaveSlot(saveSlotNumber);
        }

        currentPath = Application.persistentDataPath + "/ChrysalisSaveData" + "/SaveSlot" + saveSlotNumber + "/";
        Directory.CreateDirectory(currentPath);
        SceneManager.LoadScene("InitScene");
    }
}
