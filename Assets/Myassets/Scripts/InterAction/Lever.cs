using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour, IInteractable
{
    public Door[] connectedDoorScripts;
    [SerializeField] Animator leverAnimator;

    public void InteractFunction()
    {
        foreach (Door doorscipt in connectedDoorScripts)
        {

            if (doorscipt.isLocked == true)
            {
                doorscipt.isLocked = false;
                print("hiiiii");

                if (doorscipt.opensWhenUnlocked == true)
                {
                    doorscipt.OpenDoor();
                }
            }


            else if (doorscipt.isLocked == false)
            {
                doorscipt.isLocked = true;

                if (doorscipt.closesWhenLocked == true) 
                {
                    doorscipt.CloseDoor();
                }
            }
        }

        leverAnimator.SetTrigger("_ActivateLever");
    }
}
