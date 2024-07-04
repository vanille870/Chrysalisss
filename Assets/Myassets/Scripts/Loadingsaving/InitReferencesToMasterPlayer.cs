using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class InitStaticReferencesStorageToMaster : MonoBehaviour
{

    [SerializeField] PlayerMovement playerMovementScript;
    [SerializeField] GeneralAnimationWeapon generalAnimationWeaponScript;
    [SerializeField] Interact interactScript;
    [SerializeField] DamageNumberManager damageNumberManager;


    [SerializeField] CharacterController characterController;
    [SerializeField] PlayerStats playerStats;
    [SerializeField] MainCharAnimation mainCharAnimationScript;
    [SerializeField] PlayerInventory playerInventoryScript;
    [SerializeField] GameObject playerGO;
    [SerializeField] GameObject playerVisibilityParent;
    [SerializeField] Transform PlayerPoint; 
    [SerializeField] CinemachineVirtualCamera playerCam;

    public StaticReferencesStorage StaticReferencesStorage;

    public void InitPlayer()
    {
        StaticReferencesStorage.generalAnimationWeaponReference = generalAnimationWeaponScript;
        StaticReferencesStorage.PlayerMovementReference = playerMovementScript;
        StaticReferencesStorage.interactScriptReference = interactScript;
        StaticReferencesStorage.playerStatsReference = playerStats;
        StaticReferencesStorage.playerStatsReference = playerStats;
        StaticReferencesStorage.mainCharAnimationScript = mainCharAnimationScript;
        StaticReferencesStorage.playerInventory = playerInventoryScript;
        StaticReferencesStorage.playerController = characterController;
        StaticReferencesStorage.playerGO = playerGO;
        StaticReferencesStorage.playerCam = playerCam;
        StaticReferencesStorage.playerVisibilityParent = playerVisibilityParent;
        StaticReferencesStorage.playerPoint = PlayerPoint;
        StaticReferencesStorage.damageNumberManager = damageNumberManager;

        PlayerInventory.inventoryParentObjectStatic = StaticReferencesStorage.InventoryParent;
        ResetAfterimageShaderParams();

    }

    void ResetAfterimageShaderParams()
    {
        GetComponentInChildren<AfterMiragesPlayer>().ResetShaderParams();
    }


}
