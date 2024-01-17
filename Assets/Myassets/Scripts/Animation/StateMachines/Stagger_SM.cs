using UnityEngine;

public class Stagger_SM : StateMachineBehaviour
{
    
    public PlayerMovement playerMovementScript;

    
    void OnStateEnter()
    {
        playerMovementScript.ZeroSpeedAndTurning();
    }
}