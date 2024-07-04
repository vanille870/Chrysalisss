using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interact : MonoBehaviour
{
    [SerializeField] float interactSphereSize;
    [SerializeField] LayerMask InteractionLayerMask;
    [SerializeField] Transform interactSphereOrigin;

    [SerializeField] Collider[] InteractedColliders;

    int foundColliders;
    int i;
    Collider currentCol;

    IInteractableTextbox IinteractableText;



    public void FindInteractionObjects()
    {
        foundColliders = Physics.OverlapSphereNonAlloc(interactSphereOrigin.position, interactSphereSize, InteractedColliders);

        CheckInterface();
    }

    void PassFunctionToInput(Action<InputAction.CallbackContext> passedInteractFunction, bool isSkipFunction)
    {
        if (isSkipFunction == false)
        {
            InputManager.SetInteractFunction(passedInteractFunction);
        }

        else
        {
            InputManager.SetSkipInteractionFunction(passedInteractFunction);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactSphereOrigin.position, interactSphereSize);
    }

    void CheckInterface()
    {
        for (i = 0; i < foundColliders; i++)
        {
            if (InteractedColliders[i].gameObject.CompareTag("InteractableObject"))
            {
                if (InteractedColliders[i].gameObject.TryGetComponent<IInteractableTextbox>(out var interactableTextBox))
                {
                    currentCol = InteractedColliders[i];
                    IinteractableText = currentCol.gameObject.GetComponent<IInteractableTextbox>();

                    if (IinteractableText.IsInteractable == true)
                    {
                        if (IinteractableText.TakesControlAway == true)
                        {
                            PassFunctionToInput(IinteractableText.InitInteractionFunction(), false);
                            PassFunctionToInput(IinteractableText.InitSecondaryInteractionFunction(), true);
                        }

                        else
                        {
                            IinteractableText.InitInteractionFunction();
                        }
                    }

                    break;
                }

                else if (InteractedColliders[i].gameObject.TryGetComponent<IInteractable>(out var interactable))
                {
                    InteractedColliders[i].gameObject.GetComponent<IInteractable>().InteractFunction();
                }
            }

        }
    }
}
