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
    [SerializeField] PlayerMovement movementScript;
    [SerializeField] GeneralAnimationWeapon generalAnimationWeapon;
    [SerializeField] Interact interactionScript;
    [SerializeField] ObjectInteraction textBoxInteractionScript;

    public static Action<InputAction.CallbackContext> currentInteractFunction;

    void Awake()
    {
        playerInput = new Crystal_inputs();
    }

    //Set what each input does here, enable the first used control scheme
    void OnEnable()
    {

        playerInput.Enable();

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

        playerInput.Interacting.ContinueInteraction.Enable();
        playerInput.Interacting.Disable();
    }

    public static void SetInteractFunction(Action<InputAction.CallbackContext> interactionFunction)
    {
        playerInput.InGame.Disable();
        print("function set");

        currentInteractFunction = interactionFunction;

        playerInput.Interacting.ContinueInteraction.started -= currentInteractFunction;
        playerInput.Interacting.ContinueInteraction.started += currentInteractFunction;

        playerInput.Interacting.Enable();
    }

    public static void ResetInteractionFunction()
    {
        playerInput.Interacting.ContinueInteraction.started -= currentInteractFunction;
        print("function reset");

        playerInput.InGame.Enable();
        playerInput.Interacting.Disable();
    }
}