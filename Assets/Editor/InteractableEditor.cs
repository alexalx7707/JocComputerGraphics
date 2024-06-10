using UnityEditor;
//this tells Unity that we want to create a custom editor for the Interactable script
//the true parameter is used for child classes of Interactable to also use this custom editor
[CustomEditor(typeof(Interactable),true)] 
public class InteractableEditor : Editor
{
    //a custom editor allows us change how our scripts appear in the inspector by replacing the default layout with our own
    //this method gets called everytime Unity updates the editor interface
    public override void OnInspectorGUI()
    {
        Interactable interactable = (Interactable)target; //target is the object that this editor is inspecting

        if (target.GetType() == typeof(EventOnlyInteractable)) //if the target is an EventOnlyInteractable object
        {
            //we do not have a base.OnInspectorGUI() call here because we do not want to display the default inspector
            //we manually create our promptMessage field
            interactable.promptMessage = EditorGUILayout.TextField("Prompt Message", interactable.promptMessage);
            EditorGUILayout.HelpBox("EventOnlyInteract can ONLY use UnityEvents", MessageType.Info); 
            
            if(interactable.GetComponent<InteractionEvent>() == null)
            { 
                interactable.useEvents = true; //if the object is an EventOnlyInteractable, it will always use events
                interactable.gameObject.AddComponent<InteractionEvent>(); //add the InteractionEvent component
            }
        }
        else //if the target is an Interactable object
        {

            //this line will call the default inspector for the Interactable script
            //will be how the interactable component appears with no modifications
            base.OnInspectorGUI();
            if (interactable.useEvents) //if the interactable object is using events
            {
                if (interactable.GetComponent<InteractionEvent>() == null)
                {
                    //if the interactable object does not have an InteractionEvent component, add one
                    interactable.gameObject.AddComponent<InteractionEvent>();
                }
            }
            else
            {
                //if the interactable object is not using events, remove the InteractionEvent component
                if (interactable.GetComponent<InteractionEvent>() != null)
                {
                    DestroyImmediate(interactable.GetComponent<InteractionEvent>());
                }
            }
        }
    }
}
