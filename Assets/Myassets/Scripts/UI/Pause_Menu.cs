using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause_Menu : MonoBehaviour
{
    public static bool GameIsPaused;
    [SerializeField] InputManager inputManagerScript;
    [SerializeField] CustomGameLoop customGameLoopScript;

    [SerializeField] GameObject in_GameUI;
    [SerializeField] GameObject pause_UI;

    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject optionsMenu;

    [SerializeField] Button startButton;

    void Start()
    {
        DisbalePauseMenu();
    }

    public void TogglePause()
    {
        if (InventoryMenu.InventoryOpen == true)
        {
            return;
        }

        if (GameIsPaused == false)
        {
            Time.timeScale = 0f;
            customGameLoopScript.DisableLoop();

            GameIsPaused = true;
            inputManagerScript.ToggleMenuControls();

            in_GameUI.SetActive(false);
            pause_UI.SetActive(true);
            optionsMenu.SetActive(false);
            mainMenu.SetActive(true);

            startButton.Select();
        }

        else if (GameIsPaused == true)
        {
            inputManagerScript.ToggleMenuControls();

            DisbalePauseMenu();
        }
    }

    void DisbalePauseMenu()
    {
        in_GameUI.SetActive(true);
        pause_UI.SetActive(false);
        optionsMenu.SetActive(false);
        mainMenu.SetActive(false);
        GameIsPaused = false;
        customGameLoopScript.EnableLoop();

        Time.timeScale = 1f;
    }
}
