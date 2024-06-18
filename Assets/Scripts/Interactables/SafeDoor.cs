using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeDoor : Interactable
{
    [SerializeField]
    private GameObject safe;
    private bool safeOpen;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //here we will implement the custom code for the SafeDoor
    protected override void Interact()
    {
        if (!safeOpen)
        {
            SafeManager.Instance.safeCount++;
            safeOpen = !safeOpen; //toggle the safeOpen variable, if the safe is open, close it and vice versa
            safe.GetComponent<Animator>().SetBool("IsSafeOpen", safeOpen); //set the open parameter in the animator to the value of safeOpen
        }
    }
}
