using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private Camera cam;
    [SerializeField]
    private float distance = 3f;
    [SerializeField]
    private LayerMask mask; //this is the layer mask that will be used to filter out the objects that the ray should hit
    private PlayerUI playerUI;
    private InputManager inputManager;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<PlayerLook>().cam;
        playerUI = GetComponent<PlayerUI>();
        inputManager = GetComponent<InputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        playerUI.UpdateText(string.Empty); //clear the text on the screen (if any text is being displayed)
        //create a ray that goes from the camera to the forward direction of the camera
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * distance, Color.red);
        RaycastHit hitInfo; //this will store information about the object that was hit by the ray
        //if the ray hits an object that is on the layer mask, then store the information about the object in the hitInfo variable
        if (Physics.Raycast(ray, out hitInfo, distance, mask))
        {
            //if the object that was hit by the ray has an Interactable component, then display the prompt message on the screen
            if(hitInfo.collider.GetComponent<Interactable>() != null)
            {
                //storting the interactable in a variable to avoid calling GetComponent multiple times
                Interactable interactable = hitInfo.collider.GetComponent<Interactable>();
                playerUI.UpdateText(interactable.promptMessage);
                if(inputManager.onFoot.Interact.triggered)
                {
                    interactable.BaseInteract();
                }
            }
        }
    }
}
