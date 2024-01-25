using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [Header("Input-Setup")]
    public static Crystal_inputs playerInput;
    static public float moveDirectionX;
    static public float moveDirectionY;
    static float accelerationVelocity;
    public float acceleration;

    [Header("Scripts")]
    [SerializeField] PlayerMovement movementScript;
    [SerializeField] GeneralAnimationWeapon generalAnimationWeapon;

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

        playerInput.InGame.ChargeAttack.started += _ => generalAnimationWeapon.StartChargeAttack();
        playerInput.InGame.ChargeAttack.canceled += _ => generalAnimationWeapon.PerformChargeAttack();
        playerInput.InGame.Dodge.Enable();
        

        
    }

    public static void DeActivateAllInputMaps()
    {
  
    }

    public static void ActivateUI()
    {

    }

    public static void ActivatePlayer()
    {
       
    }
}
