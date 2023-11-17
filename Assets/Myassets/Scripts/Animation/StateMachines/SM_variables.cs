using UnityEngine;
using UnityEngine.ProBuilder;

public class SM_variables : MonoBehaviour
{
    //make space to put the scripts in
    public GeneralAnimationWeapon generalAnimationWeapon;
    public Movement movement;
    private Normal_attack normal_Attack1;

    private Animator mainCharAnimator;

    private void Awake()
    {
        this.mainCharAnimator = this.GetComponent<Animator>();

        //get all statemachines and put in the respective script
        Normal_attack[] normal_Attack_scripts = this.mainCharAnimator.GetBehaviours<Normal_attack>();
        foreach (Normal_attack normal_Attack_SM in normal_Attack_scripts)
        {
            normal_Attack_SM.generalAnimationWeapon = this.generalAnimationWeapon;
            normal_Attack_SM.movement = this.movement;
        }

        mainCharAnimator.GetBehaviour<Idle_SM>().movement = movement;
        mainCharAnimator.GetBehaviour<Idle_SM>().generalAnimationWeapon = generalAnimationWeapon;
    }
}