using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Door : MonoBehaviour, IInteractable
{
    [field: SerializeField]
    public bool TakesControlAway { get; set; }

    public bool isLocked;
    public bool opensWhenUnlocked;
    public bool closesWhenLocked;

    public bool isOpen;

    [SerializeField] Animator doorAnimator;

    public void InteractFunction()
    {
        if (isLocked == false)
        {
            doorAnimator.SetTrigger("_OpenDoor");
            isOpen = true;
        }
    }

    public void OpenDoor()
    {
        if (isOpen == false)
        {
            doorAnimator.SetTrigger("_OpenDoor");
            isOpen = true;
        }
    }

    public void CloseDoor()
    {
        if (isOpen == true)
        {
            doorAnimator.SetTrigger("_CloseDoor");
            isOpen = false;
        }

    }
}
