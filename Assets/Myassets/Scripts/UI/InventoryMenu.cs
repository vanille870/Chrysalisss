using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryMenu : MonoBehaviour
{
    public static bool InventoryOpen;
    [SerializeField] GameObject InventoryUI;
    [SerializeField] CustomGameLoop customGameLoopScript;

    [SerializeField] Button FirstButton;

    EventSystem eventSystem;


    void Awake()
    {
        print("started");
        InventoryUI.SetActive(false);
    }

    // Update is called once per frame
    public void ToggleInventory()
    {
        if (Pause_Menu.GameIsPaused == true)
        {
            print("Main menu is open");
            return;
        }

        if (InventoryOpen == false)
        {
            print("InventoryOpen");
            customGameLoopScript.DisableLoop();
            Time.timeScale = 0;
            InventoryOpen = true;

            InventoryUI.SetActive(true);

            InventoryUI.GetComponentInChildren<Button>().Select();
        }

        else
        {
            print("Inventory closed");
            customGameLoopScript.EnableLoop();
            Time.timeScale = 1;
            InventoryOpen = false;

            InventoryUI.SetActive(false);
        }
    }
}
