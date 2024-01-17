using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    public enum MovementState
    {
        NormalMovment = 0,
        AttackPush,
        NoMovemnt,
        KnockBack,
        Dodge
    }

    public MovementState currentMovementState = MovementState.NormalMovment;
    private System.Action[] runCurrentMovement = null;
    public MainCharAnimation mainCharAnimationScript;


    [Header("Bools")]
    public bool lerpSpeed;
    public bool IsPushing;
    public bool isAttacking;
    public bool playerKnockBack = false;

    [Header("Movement floats")]
    public float currentSpeed;
    public float maxSpeed;
    public float acceleration;
    public float deAcceleration;
    public float AnimAcceleration;
    public float AnimDeacceleration;
    public float mbBlendFloatDummy;
    private float currentMBVelocity;
    public float pushAmountAttack;
    public float turnSmoothTimeground;
    public float turnSmoothTimegroundOriginal;
    public float turnSmoothAttack;
    public float KnockbackTimer;
    public float KnockbackTime;
    float knockbackAmountHere;
    public float DodgeSpeed;

    private float currentVelocity;
    private float originalMaxSpeed;
    private float AccelerationOrginal;

    [Header("Jump floats")]
    public float airTimer;
    public float airSecs;
    public float originalFallSpeed;
    public float timeElapsed;
    public float lerpDuration;
    public static float turnSmoothTimeBase = 0.1f;
    public float turnSmoothTimeJump = 1f;
    public float fallSpeed;
    public float JumpForce;

    [Header("Angle")]
    public float turnAngleSkewSmoothTime;
    float targetAngle;
    float smoothingAngle;
    float turnSmoothVelocity;
    float turnSmoothVelocitySkew;
    public float skewAmount;

    [Header("Vectors 3")]
    public Vector3 moveDir;
    static public Vector2 Input;
    public Vector3 storeLastMoveDir;
    Vector3 otherPositionHere;
    Vector3 ForcedRotation;
    Vector3 DodgeDirection;

    [Header("Unity")]
    public Transform cam;
    CharacterController charControl;

    [System.Serializable]
    public struct TimedEvent
    {
        [SerializeField]
        private float Duration;
        private float Clock;

        public TimedEvent(float duration, float time = 0f)
        {
            Duration = duration;
            Clock = time;
        }

        public void SetClock()
        {
            Clock = Time.time + Duration;
        }

        public bool IsFinished => Time.time >= Clock;
    }

    [Header("Timers")]
    [SerializeField]
    private TimedEvent KnockBackTimer = new TimedEvent();
    [SerializeField]
    private TimedEvent AttackPushTimer = new TimedEvent();
    [SerializeField]
    private TimedEvent DodgeTimer = new TimedEvent();


    void Start()
    {
        charControl = gameObject.GetComponent<CharacterController>();

        turnSmoothTimegroundOriginal = turnSmoothTimeground;
        originalMaxSpeed = maxSpeed;
        AccelerationOrginal = acceleration;

        runCurrentMovement = new System.Action[]
        {
            NormalMovement,
            AttackPush,
            NotMoving,
            IncurKnockBack,
            Dodge
        };
    }

    void Update()
    {
        MovementVector3();
        //Detects movement and decides deadzone
        if (moveDir.sqrMagnitude >= 0.1f)
        {
            //smooth turning
            if (currentMovementState == MovementState.NormalMovment)
            {
                targetAngle = Mathf.Atan2(moveDir.x, moveDir.z) * Mathf.Rad2Deg;
                smoothingAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTimeground);
            }

            if (currentMovementState == MovementState.NormalMovment)
            {
                //smoothly skews angle for top down illusion
                transform.rotation = Quaternion.Euler(Mathf.SmoothDampAngle(transform.eulerAngles.x, moveDir.z * skewAmount, ref turnSmoothVelocitySkew, turnAngleSkewSmoothTime), smoothingAngle, 0f);
            }

            if (currentMovementState == MovementState.NoMovemnt)
            {
                currentMovementState = MovementState.NormalMovment;
                lerpSpeed = true;
            }

        }

        else if (currentMovementState == MovementState.NormalMovment)
        {
            currentMovementState = MovementState.NoMovemnt;
        }

        runCurrentMovement[(int)currentMovementState]();
    }

    void IncurKnockBack()
    {
        Vector3 knockbackDir = otherPositionHere - transform.position;
        knockbackDir = new Vector3(knockbackDir.x, 0, knockbackDir.z);
        knockbackDir = -knockbackDir.normalized;
        charControl.Move(knockbackDir * knockbackAmountHere * Time.deltaTime);

        //ForcedRotation = new Vector3(otherPositionHere.x, transform.position.y, otherPositionHere.z);
        //transform.LookAt(ForcedRotation);

        if (KnockBackTimer.IsFinished)
        {
            currentMovementState = MovementState.NoMovemnt;
            playerKnockBack = false;
        }
    }

    public void StartKnockBack(float amountOfKnockback, Vector3 positionOther)
    {
        KnockBackTimer.SetClock();
        currentMovementState = MovementState.KnockBack;
        otherPositionHere = positionOther;
        knockbackAmountHere = amountOfKnockback;
        playerKnockBack = true;
    }

    void NormalMovement()
    {
        //Smootly increases variable for idle state machine
        MovementVector3();
        mbBlendFloatDummy = Mathf.SmoothDamp(mbBlendFloatDummy, moveDir.magnitude, ref currentMBVelocity, AnimAcceleration);
        mbBlendFloatDummy = Mathf.Clamp(mbBlendFloatDummy, 0, 1);

        LerpSpeed();
        charControl.Move((moveDir * currentSpeed + Physics.gravity) * Time.deltaTime);
    }

    void NotMoving()
    {
        //Smootly decrease variable and character speed for idle state machine, and ensures the character moves forward
        mbBlendFloatDummy -= AnimDeacceleration * Time.deltaTime;
        mbBlendFloatDummy = Mathf.Clamp(mbBlendFloatDummy, 0, 1);

        moveDir = transform.forward;

        currentSpeed -= deAcceleration * Time.deltaTime;
        currentSpeed = Mathf.Clamp(currentSpeed, 0, Mathf.Infinity);
        charControl.Move(Physics.gravity * Time.deltaTime);
    }

    public void AttackPush()
    {

        //if attacking this pushes the character for a more weighty feel.
        if (AttackPushTimer.IsFinished!)
            charControl.Move((transform.forward * pushAmountAttack + Physics.gravity) * Time.deltaTime);

        else
            charControl.Move(Vector3.zero);
    }



    //called from statemachine
    public void CallAttackPush()
    {
        AttackPushTimer.SetClock();
        currentMovementState = MovementState.AttackPush;
        isAttacking = true;
        lerpSpeed = false;
        turnSmoothTimeground = turnSmoothAttack;

    }

    //called from statemachine
    public void FinishAttacking()
    {
        currentMovementState = MovementState.NoMovemnt;
        isAttacking = false;
        turnSmoothTimeground = turnSmoothTimegroundOriginal;

        currentSpeed = 0;
        mbBlendFloatDummy = 0;
        moveDir = Vector3.zero;
        lerpSpeed = false;
    }

    public void LerpSpeed()
    {
        //smootly increase speed to max
        if (lerpSpeed == true)
        {
            currentSpeed = Mathf.SmoothDamp(currentSpeed, maxSpeed, ref currentVelocity, -acceleration);
        }
    }

    public void StopMovement()
    {
        lerpSpeed = false;

        if (currentMovementState == MovementState.NormalMovment)
            currentMovementState = MovementState.NoMovemnt;
    }

    public void StartMoving()
    {
        if (currentMovementState == MovementState.NoMovemnt)
        {
            lerpSpeed = true;
            currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);
            MovementVector3();
            currentMovementState = MovementState.NormalMovment;
        }
    }


    public void MovementVector3()
    {
        //reads new input system vector and puts in in the moveDir variable
        Input = InputManager.playerInput.InGame.Movement.ReadValue<Vector2>();
        moveDir = new Vector3(Input.x, 0, Input.y);
    }

    public void StartDodge()
    {
        if (moveDir.sqrMagnitude >= 0.1f && currentMovementState == MovementState.NormalMovment)
        {
            turnSmoothTimeground = 0;
            currentMovementState = MovementState.Dodge;
            DodgeDirection = charControl.velocity;
            DodgeTimer.SetClock();
        }
    }

    public void Dodge()
    {
        charControl.Move((DodgeDirection * DodgeSpeed + Physics.gravity) * Time.deltaTime);


        if (DodgeTimer.IsFinished)
        {
            currentMovementState = MovementState.NoMovemnt;
        }
    }

    public void ZeroSpeedAndTurning()
    {
        maxSpeed = 0;
        turnSmoothTimeground = 999999;
        currentSpeed = 0;
        acceleration = -99999;
    }

    public void RestoreSpeedAndTurning()
    {
        maxSpeed = originalMaxSpeed;
        turnSmoothTimeground = turnSmoothTimegroundOriginal;
        acceleration = AccelerationOrginal;
    }
}


