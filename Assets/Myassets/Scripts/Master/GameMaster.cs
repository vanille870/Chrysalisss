using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public StaticReferencesStorage StaticReferencesStorageSO;

    [Header("Set in inspector")]
    public InputManager inputManagerScript;
    public TextBox textBoxScript;
    public GameObject InventoryParent;
    public GameObject DamageNumbrPool;
    public GameObject grassParticlSystemPool;
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

    private void Awake()
    {
        StaticReferencesStorageSO.inputManagerScript = inputManagerScript;

        StaticReferencesStorageSO.PlayerMovementReference = playerMovementScript;
        StaticReferencesStorageSO.textBoxScript = textBoxScript;
        StaticReferencesStorageSO.DamageNumbrPool = DamageNumbrPool;
        StaticReferencesStorageSO.grassParticlSystemPool = grassParticlSystemPool;
        StaticReferencesStorageSO.InventoryParent = InventoryParent;
    }

    public void MovePlayer(Vector3 TargetPosition)
    {
        StaticReferencesStorageSO.playerTrans.position = TargetPosition;
        print("teleport");
    }

}
