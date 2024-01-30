using UnityEngine;

public class SM_variables : MonoBehaviour
{
    //make space to put the scripts in
    public GeneralAnimationWeapon generalAnimationWeapon;
    public PlayerMovement movement;

    private Animator mainCharAnimator;


    private void Awake()
    {
        mainCharAnimator = GetComponent<Animator>();

        //get all statemachines and put in the respective script
        Normal_attack[] normal_Attack_scripts = this.mainCharAnimator.GetBehaviours<Normal_attack>();
        foreach (Normal_attack normal_Attack_SM in normal_Attack_scripts)
        {
            normal_Attack_SM.generalAnimationWeapon = this.generalAnimationWeapon;
            normal_Attack_SM.movement = this.movement;
        }

        Stagger_SM[] StaggerScripts = mainCharAnimator.GetBehaviours<Stagger_SM>();
        foreach (Stagger_SM staggerSM in StaggerScripts)
        {
            staggerSM.playerMovementScript = this.movement;
        }

        mainCharAnimator.GetBehaviour<Idle_SM>().SMmovement = movement;
        mainCharAnimator.GetBehaviour<Idle_SM>().generalAnimationWeapon = generalAnimationWeapon;

    }
}