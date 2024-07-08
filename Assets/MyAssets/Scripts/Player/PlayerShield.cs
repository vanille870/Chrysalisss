using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerShield : MonoBehaviour
{
    BlendTree BlendTree;
    Animation shieldAnim;
    [SerializeField] Animator animator;

    [SerializeField]
    LerpEvent lerpWeight = new LerpEvent();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShieldUp()
    {
        animator.SetLayerWeight(1, 1);
    }

    public void ShieldDown()
    {
        animator.SetLayerWeight(1, 0);
    }

    void ShieldUpLerp()
    {
        lerpWeight.Lerp();
        animator.SetLayerWeight(1, lerpWeight.LerpFloat);
    }
}
