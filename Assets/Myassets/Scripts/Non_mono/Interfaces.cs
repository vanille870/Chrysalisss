using UnityEngine;
using System;
using UnityEngine.InputSystem;


public interface IBreakable
{
   void Damage (float damage, float range, Vector3 position);
}

public interface IInteractable
{
   public void InteractFunction();
}

public interface IInteractableTextbox
{
    Action<InputAction.CallbackContext> InitInteractionFunction();
    Action<InputAction.CallbackContext> InitSecondaryInteractionFunction();
    public bool TakesControlAway { get; set; }
    public bool IsInteractable { get; set; }
}