using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{


    [Header("Bools")]
    public bool lerpSpeed;
    public bool isMoving = false;
    public bool isFallingFromJump;
    public bool isSprinting;
    public bool isHoldingJump;
    private bool multiplySpeed;
    public bool IsPushing;
    public bool isAttacking;

    [Header("Movement floats")]
    public float acceleration;
    public float AnimAcceleration;
    public float currentSpeed;
    private float currentVelocity;
    public float deAcceleration;
    private Vector3 currentVectorVelocity;
    public float vectorVelocity;
    public float speedDA;
    public float maxSpeed;
    public float maxSprintSpeed;
    public float mBFloatDummy;
    public float mBBlendFloatacceleration;
    public float mBFloatDeacceleration;
    private float currentMBVelocity;
    public float pushAmountAttack;
    private float smoothTurnStorage;
    public float turnSmoothTimeground;
    public float turnSmoothAttack;
    public float pushTimer;
    public float pushTime;


    [Header("Jump floats")]
    public float airTimer;
    public float airSecs;
    public float originalFallSpeed;
    public float timeElapsed;
    public float lerpDuration;
    float smoothDampingCamera;
    public static float turnSmoothTimeBase = 0.1f;
    public float turnSmoothTimeJump = 1f;
    public float fallSpeed;
    public float JumpForce;
    bool playerGrounded;
    bool isJumping;

    [Header("Angle")]
    public float turnAngleSkewSmoothTime;
    float turSmoothVelocity;
    float targetAngle;
    float smoothingAngle;
    float turnSmoothVelocity;
    float turnSmoothVelocitySkew;
    public float skewAmount;

    [Header("Vectors 3")]
    static public Vector3 moveDir;
    public Vector3 moveDirShowInEditor;
    private Vector3 playerVelocity;
    static public Vector2 Input;
    public Vector3 storeLastMoveDir;

    [Header("Unity")]
    public Transform cam;
    CharacterController charControl;
    CapsuleCollider capCol;





    void Start()
    {
        charControl = gameObject.GetComponent<CharacterController>();
        capCol = gameObject.GetComponent<CapsuleCollider>();
        turnSmoothTimeBase = turnSmoothTimeground;
        smoothTurnStorage = turnSmoothTimeground;
    }

    /*public void EndJump()
    {
        isHoldingJump = false;
        isFallingFromJump = true;
        airTimer = 0;
    }*/

    void Update()
    {
        //Detects movement and decides deadzone
        if (moveDir.sqrMagnitude >= 0.1f)
        {
            //smooth turning
            targetAngle = Mathf.Atan2(moveDir.x, moveDir.z) * Mathf.Rad2Deg;
            smoothingAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTimeground);

            if (isAttacking == false)
            {
                //smoothly skews angle for top down illusion
                transform.rotation = Quaternion.Euler(Mathf.SmoothDampAngle(transform.eulerAngles.x, moveDir.z * skewAmount, ref turnSmoothVelocitySkew, turnAngleSkewSmoothTime), smoothingAngle, 0f);
            }

        }

        moveDirShowInEditor = moveDir;


        /*if (Pause.gameIsPaused)
        {
            return;
        }*/

        //GroundCheck();
        //detremines how long you can hold jump to increase height


        PlayerMovement();
        AttackPushtimer();

        //PlayerJumpUpdateFunctions();
    }

    void GroundCheck()
    {
        playerGrounded = charControl.isGrounded;
    }

    public void StartJump()
    {
        /*if (playerGrounded && !ToggleInventoryScreen.GameScreensOpen)
        {

            isJumping = true;
            isHoldingJump = true;
            turnSmoothTimeground = turnSmoothTimeJump;
            playerVelocity = Vector3.up * JumpForce;
            originalFallSpeed = fallSpeed;
        }*/

    }

    public void Sprinting()
    {
        isSprinting = !isSprinting;
    }

    public void MovementVector3()
    {
        //reads new input system vector and puts in in the moveDir variable
        Input = InputManager.playerInput.InGame.Movement.ReadValue<Vector2>();
        moveDir = new Vector3(Input.x, 0, Input.y);
    }

    void PlayerMovement()
    {
        //Holds player to the ground
        //playerVelocity += Physics.gravity * Time.deltaTime * fallSpeed;

        /* if (playerGrounded && playerVelocity.y <= 0)
         {
             playerVelocity.y = 0f;
             isJumping = false;
             //turnSmoothTimeground = turnSmoothTimeBase;

         }*/

        if (isMoving == true)
        {
            //Smootly increases variable for idle state machine
            MovementVector3();
            mBFloatDummy = Mathf.SmoothDamp(mBFloatDummy, moveDir.magnitude, ref currentMBVelocity, AnimAcceleration);
            mBFloatDummy = Mathf.Clamp(mBFloatDummy, 0, 1);
        }

        else
        {
            //Smootly increases variable and character speed for idle state machine, and ensures the character moves forward
            mBFloatDummy -= mBFloatDeacceleration * Time.deltaTime;
            mBFloatDummy = Mathf.Clamp(mBFloatDummy, 0, 1);

            moveDir = transform.forward;

            currentSpeed -= deAcceleration * Time.deltaTime;
            currentSpeed = Mathf.Clamp(currentSpeed, 0, Mathf.Infinity);
        }

        LerpSpeed();


        if (isAttacking == false)
        {
            //if not attacking regular movemnt is performed
            if (!isSprinting)
                charControl.Move((moveDir * currentSpeed + ((Physics.gravity / 5)) + playerVelocity) * Time.deltaTime);

            else
                charControl.Move((moveDir * maxSprintSpeed + ((Physics.gravity / 5)) + playerVelocity) * Time.deltaTime);
        }

        else if (IsPushing == true)
        {
            //if attacking this pushes the character for a more weighty feel.
            charControl.Move((transform.forward * pushAmountAttack + ((Physics.gravity / 5)) + playerVelocity) * Time.deltaTime);
        }


    }

    //used for hold jump mechanic
    void PlayerJumpUpdateFunctions()
    {
        if (isHoldingJump)
            playerVelocity = Vector3.up * JumpForce;


        if (isHoldingJump)
            if (JumpairTimer(airSecs))
            {
                isHoldingJump = false;
                isFallingFromJump = true;
                airTimer = 0;
            }


    }

    public bool JumpairTimer(float sec)
    {
        airTimer += Time.deltaTime;
        return (airTimer >= sec);

    }

    public void test(int teeeest)
    {
        teeeest = 5;
    }

    public void LerpSpeed()
    {
        //smootly increase speed to max
        if (lerpSpeed == true)
        {
            currentSpeed = Mathf.SmoothDamp(currentSpeed, maxSpeed, ref currentVelocity, acceleration);
        }
    }

    public void StopMovement()
    {
        //smootly increase speed to max
        if (isAttacking == false)
        {
            lerpSpeed = false;
            isMoving = false;
        }
    }

    public void StartMoving()
    {
        isMoving = true;
        lerpSpeed = true;
        currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);
    }

    public void PushOnAttack()
    {
        currentSpeed = pushAmountAttack;
        IsPushing = true;
        print("pushhhhhhhh");
    }

    public void AttackMovementModeStart()
    {
        isAttacking = true;
        currentSpeed = 0;
        smoothTurnStorage = turnSmoothTimeground;
        turnSmoothTimeground = turnSmoothAttack;
    }

    public void AttackMovementModeStop()
    {
        turnSmoothTimeground = smoothTurnStorage;
        isAttacking = false;
        currentSpeed = 0;
        print("stopp");
    }

    public void AttackPushtimer()
    {
        if (IsPushing == true)
        {
            pushTimer += Time.deltaTime;

            if (pushTimer >= pushTime)
            {
                pushTimer = 0;
                IsPushing = false;
            }
        }
    }
}


