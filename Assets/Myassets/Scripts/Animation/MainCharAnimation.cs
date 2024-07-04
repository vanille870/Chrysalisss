
using UnityEngine;


public class MainCharAnimation : MonoBehaviour
{
    public Animator mainCharAnimator;
    public PlayerMovement movementScript;

    [Range(0, 1)]
    public float movementBlendFloat;

     void OnEnable()
    {
        CustomGameLoop.LateupdateLoopFunctionsSubscriber += UpdateMovementBlendFloat;
    }
    
    void OnDisable()
    {
        CustomGameLoop.LateupdateLoopFunctionsSubscriber -= UpdateMovementBlendFloat;
    }

    // Update is called once per frame
    void UpdateMovementBlendFloat()
    {       
        movementBlendFloat = movementScript.mbBlendFloatDummy;
        mainCharAnimator.SetFloat("MovementBlend", movementBlendFloat);
    }

    public void Stagger()
    {
        mainCharAnimator.SetTrigger("_StaggerFront");
    }

    public void StaggerBack()
    {
        mainCharAnimator.SetTrigger("_StaggerBack");
    }

    public void Dodge()
    {
        mainCharAnimator.SetTrigger("_Dodge");
    }

    public void DodgeFinish()
    {
        mainCharAnimator.SetTrigger("_DodgeFinish");
    }

    public void ToggleFallInPit()
    {
       mainCharAnimator.SetBool("_ToggleFallInPit", !mainCharAnimator.GetBool("_ToggleFallInPit")); 
       print("fall toggled");
    }
}
