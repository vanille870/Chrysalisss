using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemPickup : MonoBehaviour, IInteractableTextbox
{
    public int ID;
    [SerializeField] PlayerInventory playerInventoryScript;
    [SerializeField] InventoryItemsData itemToGive;
    [SerializeField] Animator animator;
    [SerializeField] SaveObjects saveObjectsScript;

    [SerializeField] Transform playerPos;
    [SerializeField] TextBox textBoxScript;
    [SerializeField] string[] textArrayItem;
    [SerializeField] string[] textArrayNoItem;
    [SerializeField] string[] notInFront;
    [SerializeField] float TypeWritingSpeed;
    [SerializeField] int typeWritingFasterSpeed;
    [SerializeField] bool HasSkipFunction;
    [SerializeField] bool isContainer;

    [field: SerializeField]
    public bool TakesControlAway { get; set; }
    public bool IsInteractable { get; set; }

    [Header("Debug")]
    [SerializeField] float degreeToPlayer;

    void Start()
    {
        IsInteractable = true;
    }

    // Start is called before the first frame update
    public void OpenChestAndObtainItem()
    {
        degreeToPlayer = Vector3.Angle(transform.forward, (playerPos.position - transform.position).normalized);

        if (Mathf.Clamp(degreeToPlayer, 115, 150) == degreeToPlayer)
        {
            if (playerInventoryScript.AddItemToInventoryIfRoom(itemToGive))
            {
                textBoxScript.InitTextBox(TypeWritingSpeed, typeWritingFasterSpeed, textArrayItem, TextStyles.Item);
                animator.SetTrigger("_Open");
                IsInteractable = false;
                saveObjectsScript.Update2StateObject(TwoStateType.chest, ID);
            }

            else
            {
                textBoxScript.InitTextBox(TypeWritingSpeed, typeWritingFasterSpeed, textArrayNoItem, TextStyles.Normal);
            }
        }

        else
        {
            textBoxScript.InitTextBox(TypeWritingSpeed, typeWritingFasterSpeed, notInFront, TextStyles.Normal);
        }
    }

    public System.Action<InputAction.CallbackContext> InitInteractionFunction()
    {
        OpenChestAndObtainItem();
        return Confirm;
    }

    public System.Action<InputAction.CallbackContext> InitSecondaryInteractionFunction()
    {
        if (HasSkipFunction)
            return SkipInteraction;

        else
            return null;
    }

    public void Confirm(InputAction.CallbackContext ctx)
    {
        textBoxScript.ContinueText();
    }

    void SkipInteraction(InputAction.CallbackContext ctx)
    {
        textBoxScript.SkipText();
    }

    public void InitStaticReferencesStorage(PlayerInventory InputPlayerInventory, Transform InputPlayerPos, TextBox InputTextBoxScript)
    {
        playerInventoryScript = InputPlayerInventory;
        playerPos = InputPlayerPos;
        textBoxScript = InputTextBoxScript;
    }

    public void OpenOnLoad()
    {
        animator.SetTrigger("_Open");
        IsInteractable = false;
        print("sesam ooooooooooooooopen");
    }
}
