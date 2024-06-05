using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keypad : Interactable
{
    [SerializeField]
    private GameObject door;
    private bool doorOpen;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //this function is where the interaction with the object will be defined
    protected override void Interact()
    {
        doorOpen = !doorOpen; //toggle the doorOpen variable, if the door is open, close it and vice versa
        door.GetComponent<Animator>().SetBool("IsOpen", doorOpen); //set the open parameter in the animator to the value of doorOpen
    }
}
