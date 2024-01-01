using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralAnimationWeapon : MonoBehaviour
{
    public Animator mainCharAnimator;
    public PlayerMovement movement;
    public float speedStorage;
    public static bool isAttacking;
    public bool isAttacking_DEBUG;
    public bool isRessetingSpeed = false;
    public bool returnToIdle = false;

    public float speedTimer { get; private set; } = 0;
    public float waitInSeconds;


    // Start is called before the first frame update
    void Start()
    {
        this.TryGetComponent<Animator>(out mainCharAnimator);
        Normal_attack[] behaviours = mainCharAnimator.GetBehaviours<Normal_attack>();

        mainCharAnimator = gameObject.GetComponent<Animator>();

        for( int i = 0; i < behaviours.Length; ++i )
        {
            // Use += to register your method to the action/event.
            behaviours[i].onStateEnterOR += ResetSpeedTimer; // ResetSpeed isn't called here, it will be called when Invoke() happens above.
        }
    }

    // Update is called once per frame
    void Update()
    {
        isAttacking_DEBUG = isAttacking;
        ResetSpeedVariable();

    }

    public void StartNormalAttacks()
    {
        mainCharAnimator.SetBool("NormalAttack", true);
        isAttacking = true;
    }

    public void ResetSpeedVariable()
    {
        if (isRessetingSpeed == true)
        {
            if (ResetSpeedTimerBool(waitInSeconds))
            {
                isAttacking = false;
                isRessetingSpeed = false;
                mainCharAnimator.SetBool("ReturnToIdle", true);
                speedTimer = 0;
            }
        }

    }

    public void ResetSpeedTimer(Animator animator, AnimatorStateInfo animatorStateInfo)
    {
        speedTimer = 0;
        isRessetingSpeed = false;
    }

    public bool ResetSpeedTimerBool(float sec)
    {
        speedTimer += Time.deltaTime;
        return (speedTimer >= sec);
    } 
}
