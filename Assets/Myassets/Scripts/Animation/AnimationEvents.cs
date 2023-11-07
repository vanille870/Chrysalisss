using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{

    public GeneralAnimationWeapon generalAnimationWeapon;
    public Animator mainCharAnim;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CanStartNextAttack()
    {
        mainCharAnim.SetBool("CanStartNextAttack", true);
        generalAnimationWeapon.isRessetingSpeed = true;
        //generalAnimationWeapon.speedTimer = 0;

    }
}
