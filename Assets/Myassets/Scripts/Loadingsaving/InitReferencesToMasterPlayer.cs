using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class InitReferencesToMaster : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovementScript;
    [SerializeField] GeneralAnimationWeapon generalAnimationWeaponScript;
    [SerializeField] Interact interactScript;
    

    [SerializeField] CharacterController characterController;
    [SerializeField] PlayerConditionStats playerConditionStatsScript;
    [SerializeField] MainCharAnimation mainCharAnimationScript;
    [SerializeField] PlayerInventory playerInventoryScript;
    [SerializeField] GameObject playerGO;
    [SerializeField] GameObject playerVisibilityParent;
    [SerializeField] CinemachineVirtualCamera playerCam;

    private GameMaster singleton;

    private InputManager inputManagerSciptCache;


    public void InitPlayer()
    {
        singleton = GameMaster.gameMasterSingleton;

        singleton.generalAnimationWeaponReference = generalAnimationWeaponScript;
        singleton.PlayerMovementReference = playerMovementScript;
        singleton.interactScriptReference = interactScript;
        singleton.playerConditionStatsScript = playerConditionStatsScript;
        singleton.playerConditionStatsScript = playerConditionStatsScript;
        singleton.mainCharAnimationScript = mainCharAnimationScript;
        singleton.playerInventory = playerInventoryScript;
        singleton.playerController = characterController;
        singleton.playerGO = playerGO;
        singleton.playerCam = playerCam;
        singleton.playerVisibilityParent = playerVisibilityParent;

        PlayerInventory.inventoryParentObjectStatic = singleton.InventoryParent;
        ResetAfterimageShaderParams();

    }

    void ResetAfterimageShaderParams()
    {
        GetComponentInChildren<AfterMiragesPlayer>().ResetShaderParams();
    }


}
