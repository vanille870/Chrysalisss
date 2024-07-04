using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

[CreateAssetMenu(fileName = "Sword", menuName = "ScriptableObjects/Data/StaticReferencesStorage", order = 1)]
public class StaticReferencesStorage : ScriptableObject
{
    public StatsStorage StatsStorageSO;
    public InputManager inputManagerScript;
    public TextBox textBoxScript;
    public DamageNumberManager damageNumberManager;
    public GameObject InventoryParent;

    [Header("Pools parent game objects")]
    public GameObject DamageNumbrPool;
    public GameObject grassParticlSystemPool;

    public CharacterController playerController;
    public GameObject playerGO;
    public Transform playerPoint;
    public GameObject playerVisibilityParent;
    public Transform playerTrans;
    public MainCharAnimation mainCharAnimationScript;
    public PlayerInventory playerInventory;
    public CinemachineVirtualCamera playerCam;

    private PlayerStats playerStats;
    public PlayerStats playerStatsReference
    {
        get => playerStats;

        set
        {
            playerStats = value;
            StatsStorageSO.playerStatsScript = value;
            
        }

    }

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
}
