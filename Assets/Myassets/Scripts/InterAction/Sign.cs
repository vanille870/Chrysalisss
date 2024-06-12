using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine.InputSystem;


public class Sign : MonoBehaviour, IInteractableTextbox
{
    public TextBox textBoxScript;
    public Interact interactScript;
    public Transform playerPos;
    
    [SerializeField] string[] textArrayFront;
    [SerializeField] string[] textArraySide;
    [SerializeField] string[] textArrayBack;
    [SerializeField] int typeWritingFasterSpeed;

    [Range(0, 200)]
    [SerializeField] int currentVisibleChars;

    [SerializeField] float TypeWritingSpeed = 1;

    [SerializeField] bool test;

    [field: SerializeField]
    public bool TakesControlAway { get; set; }
    public bool IsInteractable { get; set; }

    [SerializeField] bool HasSkipFunction;


    public float degreeToPlayer;

    void Start()
    {
        IsInteractable = true;
    }

    public System.Action<InputAction.CallbackContext> InitInteractionFunction()
    {
        CheckPlayerPos();

        return ReadSign;
    }

    public System.Action<InputAction.CallbackContext> InitSecondaryInteractionFunction()
    {
        if (HasSkipFunction)
            return SkipInteraction;

        else
            return null;
    }

    public void ReadSign(InputAction.CallbackContext ctx)
    {
        textBoxScript.ContinueText();
    }

    void SkipInteraction(InputAction.CallbackContext ctx)
    {
        textBoxScript.SkipText();
    }

    void CheckPlayerPos()
    {
        degreeToPlayer = Vector3.Angle(transform.forward, (playerPos.position - transform.position).normalized);

        if (Mathf.Clamp(degreeToPlayer, 65, 100) == degreeToPlayer)
        {
            textBoxScript.InitTextBox(TypeWritingSpeed, typeWritingFasterSpeed, textArraySide, TextStyles.Normal);
        }

        else if (Mathf.Clamp(degreeToPlayer, 0, 75) == degreeToPlayer)
        {
            textBoxScript.InitTextBox(TypeWritingSpeed, typeWritingFasterSpeed, textArrayBack, TextStyles.Normal);
        }

        else
        {
            textBoxScript.InitTextBox(TypeWritingSpeed, typeWritingFasterSpeed, textArrayFront, TextStyles.Normal);
        }
    }
}
