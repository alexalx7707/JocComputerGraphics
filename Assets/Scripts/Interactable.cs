using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    //message displayed when player is in range of the interactable and is looking at it
    public string promptMessage;

    public void BaseInteract()
    {
        Interact(); 
    }
    protected virtual void Interact()
    {
        //we won't have any code here, but we will have it in the children classes
    }
}
