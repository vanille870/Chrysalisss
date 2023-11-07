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
    [SerializeField] Movement movementScript;
    [SerializeField] GeneralAnimationWeapon generalAnimationWeapon;

    void Awake()
    {
        playerInput = new Crystal_inputs();
    }

    //Set what each input does here, enable the first used control scheme
    void OnEnable()
    {

        playerInput.Enable();

        playerInput.InGame.Movement.performed += _ => movementScript.StartMoving();
        playerInput.InGame.Movement.Enable();

        playerInput.InGame.Movement.canceled += _ => movementScript.MovementVector3();
        playerInput.InGame.Movement.canceled += _ => movementScript.StopMovement();
        playerInput.InGame.Movement.Enable();

        playerInput.InGame.Normal_attack.performed += _ => generalAnimationWeapon.StartNormalAttacks();
        playerInput.InGame.Movement.Enable();
        

        
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
