using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public interface IInteractable
{
    Action<InputAction.CallbackContext> InitInteractionFunction();
}

public class Interact : MonoBehaviour
{
    [SerializeField] float interactSphereSize;
    [SerializeField] LayerMask InteractionLayerMask;
    [SerializeField] Transform interactSphereOrigin;

    [SerializeField] Collider[] InteractedColliders;


    public void FindInteractionObjects()
    {
        Physics.OverlapSphereNonAlloc(interactSphereOrigin.position, interactSphereSize, InteractedColliders);
        print("finding objects to interact with");

        foreach (Collider col in InteractedColliders)
        {
            if (col != null && col.gameObject.CompareTag("InteractableObject"))
            {
                PassFunctionToInput( col.gameObject.GetComponent<IInteractable>().InitInteractionFunction());
                Array.Clear(InteractedColliders, 0, InteractedColliders.Length);
                print("found object");
                break;
            }
        }

        Array.Clear(InteractedColliders, 0, InteractedColliders.Length);
    }

    void PassFunctionToInput(Action<InputAction.CallbackContext> passedInteractFunction)
    {
        InputManager.SetInteractFunction(passedInteractFunction);
        print("Passing function");
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactSphereOrigin.position, interactSphereSize);
    }
}
