using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [Header("Input-Setup")]
    public static Crystal_inputs playerInput;

    [Header("Scripts")]
    public PlayerMovement movementScript;
    public GeneralAnimationWeapon generalAnimationWeapon;
    public Interact interactionScript;
    public PlayerShield playerShieldScript;
    [SerializeField] Pause_Menu pause_MenuScript;
    [SerializeField] InventoryMenu inventoryMenuScript;


    public static Action<InputAction.CallbackContext> currentInteractFunction;
    public static Action<InputAction.CallbackContext> currentSkipInteractionFunction;

    void Awake()
    {
        playerInput = new Crystal_inputs();

        playerInput.InGame.Enable();
        playerInput.Interacting.Disable();
        playerInput.UI.Disable();
    }

    //Set what each input does here, enable the first used control scheme
    void OnEnable()
    {
        playerInput.InGame.Movement.started += _ => movementScript.StartMoving();
        playerInput.InGame.Movement.Enable();

        playerInput.InGame.Movement.canceled += _ => movementScript.MovementVector3();
        playerInput.InGame.Movement.canceled += _ => movementScript.StopMovement();
        playerInput.InGame.Movement.Enable();

        playerInput.InGame.Normal_attack.started += _ => generalAnimationWeapon.StartNormalAttacks();
        playerInput.InGame.Movement.Enable();

        playerInput.InGame.Dodge.started += _ => movementScript.StartDodge();
        playerInput.InGame.Dodge.Enable();

        playerInput.InGame.ChargeAttack.started += _ => generalAnimationWeapon.SetChargeAttackInAnimator();
        playerInput.InGame.ChargeAttack.canceled += _ => generalAnimationWeapon.PerformChargeAttack();
        playerInput.InGame.Dodge.Enable();

        playerInput.InGame.Interact.started += _ => interactionScript.FindInteractionObjects();
        playerInput.InGame.Dodge.Enable();

        playerInput.InGame.Pause.started += _ => pause_MenuScript.TogglePause();
        playerInput.InGame.Pause.Enable();

        playerInput.InGame.OpenInventory.started += _ => inventoryMenuScript.ToggleInventory();
        playerInput.InGame.OpenInventory.Enable();

        playerInput.InGame.RaiseShield.started += _ => playerShieldScript.ShieldUp();
        playerInput.InGame.RaiseShield.canceled += _ => playerShieldScript.ShieldDown();
        playerInput.InGame.RaiseShield.Enable();

        playerInput.UI.CloseInventory.started += _ => inventoryMenuScript.ToggleInventory();
        playerInput.UI.CloseInventory.Enable();

        playerInput.Disable();
    }

    public static void SetInteractFunction(Action<InputAction.CallbackContext> interactionFunction)
    {
        playerInput.InGame.Disable();

        currentInteractFunction = interactionFunction;

        playerInput.Interacting.ContinueInteraction.started -= currentInteractFunction;
        playerInput.Interacting.ContinueInteraction.started += currentInteractFunction;

        playerInput.Interacting.Enable();
    }

    public static void SetSkipInteractionFunction(Action<InputAction.CallbackContext> skipInteractionFunction)
    {
        currentSkipInteractionFunction = skipInteractionFunction;

        playerInput.Interacting.SkipInterAction.started -= currentSkipInteractionFunction;
        playerInput.Interacting.SkipInterAction.started += currentSkipInteractionFunction;
    }

    public static void ResetInteractionFunction()
    {
        playerInput.Interacting.ContinueInteraction.started -= currentInteractFunction;
        playerInput.Interacting.SkipInterAction.started -= currentSkipInteractionFunction;

        playerInput.InGame.Enable();
        playerInput.Interacting.Disable();

        currentInteractFunction = null;
        currentSkipInteractionFunction = null;
    }

    public void ToggleMenuControls()
    {
        if (Pause_Menu.GameIsPaused == false)
        {
            playerInput.InGame.Disable();
            playerInput.Interacting.Disable();
            playerInput.UI.Enable();
        }

        else if (Pause_Menu.GameIsPaused == true)
        {
            playerInput.InGame.Enable();
            playerInput.Interacting.Disable();
            playerInput.UI.Disable();
        }
    }

    public static void EnableControls()
    {
        InputManager.playerInput.Enable();
        playerInput.InGame.Enable();
    }

    public void EnableControlsNonStatic()
    {
        InputManager.playerInput.Enable();
        playerInput.InGame.Enable();
    }

    public void DisableControls()
    {
        InputManager.playerInput.Disable();
        playerInput.InGame.Disable();
    }

    public void ToggleIngameInput()
    {
        if (playerInput.InGame.enabled)
        {
            playerInput.InGame.Disable();
        }

        else
        {
            playerInput.InGame.Enable();
        }
    }
}