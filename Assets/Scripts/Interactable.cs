using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    //add or remove an InteractionEvent component to the interactable object
    public bool useEvents;
    //message displayed when player is in range of the interactable and is looking at it
    public string promptMessage;

    //this method will be called by the player when they are in range of the interactable object
    public void BaseInteract() 
    {
        //if the interactable object is using events, call the OnInteract event
        if(useEvents)
            GetComponent<InteractionEvent>().OnInteract.Invoke(); //should never be null because of the custom editor
        Interact(); 
    }
    protected virtual void Interact()
    {
        //we won't have any code called from the base interactable script
        //any custom code will be implemented in child classes
    }
}
