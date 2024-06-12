using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster gameMasterSingleton;

    public CharacterController playerController;
    public GameObject playerGO;
    public Transform playerTrans;
    public InputManager inputManagerScript;
    public TextBox textBoxScript;
    public GameObject InventoryParent;
    public PlayerConditionStats playerConditionStatsScript;
    public MainCharAnimation mainCharAnimationScript;
    public PlayerInventory playerInventory;
    public CinemachineVirtualCamera playerCam;

    private PlayerMovement playerMovementScript;
    public PlayerMovement PlayerMovementReference
    {
        get => playerMovementScript;

        set
        {
            playerMovementScript = value;
            inputManagerScript.movementScript = value;
        }

    }

    private GeneralAnimationWeapon generalAnimationWeapon;
    public GeneralAnimationWeapon generalAnimationWeaponReference
    {
        get => generalAnimationWeapon;

        set
        {
            generalAnimationWeapon = value;
            inputManagerScript.generalAnimationWeapon = value;
        }
    }

    public Interact interactScript;
    public Interact interactScriptReference
    {
        get
        {
            return interactScript;
        }

        set
        {
            interactScript = value;
            inputManagerScript.interactionScript = value;
        }
    }


    private void Awake()
    {
        //ensures there's only 1 instance
        if (gameMasterSingleton != null && gameMasterSingleton != this)
        {
            Destroy(this);
        }

        else
        {
            gameMasterSingleton = this;
        }

        gameMasterSingleton.playerController = playerController;
        gameMasterSingleton.playerTrans = playerTrans;
        gameMasterSingleton.inputManagerScript = inputManagerScript;
        gameMasterSingleton.playerConditionStatsScript = playerConditionStatsScript;
        gameMasterSingleton.mainCharAnimationScript = mainCharAnimationScript;
        gameMasterSingleton.playerMovementScript = playerMovementScript;
        gameMasterSingleton.textBoxScript = textBoxScript;
        gameMasterSingleton.playerInventory = playerInventory;
        gameMasterSingleton.InventoryParent = InventoryParent;
        gameMasterSingleton.playerGO = playerGO;
        gameMasterSingleton.playerCam = playerCam;
    }

    public void MovePlayer(Vector3 TargetPosition)
    {
        gameMasterSingleton.playerTrans.position = TargetPosition;
        print("teleport");
    }
    
}
