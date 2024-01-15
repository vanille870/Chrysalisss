using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Animations;

public class MainCharAnimation : MonoBehaviour
{
    public Animator mainCharAnimator;
    public BlendTree MovementBlend;
    public PlayerMovement movementScript;

    [Range(0, 1)]
    public float movementBlendFloat;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {       
        movementBlendFloat = movementScript.mbBlendFloatDummy;
        mainCharAnimator.SetFloat("MovementBlend", movementBlendFloat);
    }

    void AlignMoventBlendFloat()
    {
        
    }
}
