using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
//CENTRAL POINT WHERE I MANAGE ALL OF THE INPUTS
public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput; //reference to the generated C# class that we created OnFoot and with actions
    private PlayerInput.OnFootActions onFootActions; //reference to the OnFoot action map(walk and jump etc.)
    private PlayerMotor playerMotor;
    private PlayerLook playerLook;

    void Awake()
    {
        playerInput = new PlayerInput();
        onFootActions = playerInput.OnFoot;
        playerMotor = GetComponent<PlayerMotor>();
        //we use a lambda for jump (=>)
        //esentially we say 'when the jump action is performed (space is pressed, as in the action map),call
        //the callback context to point and execute the jump function
        //inside the player motor
        //we are registering the event handler before the game starts
        onFootActions.Jump.performed += ctx => playerMotor.Jump(); //ctx has contextual information regarding 
                                                                               //the triggered event such as input value, control device etc.
        playerLook = GetComponent<PlayerLook>();  
        
        onFootActions.Crouch.performed += ctx => playerMotor.Crouch();
    }

    // Update is called once per frame
    void FixedUpdate() //updates at a fixed rate rather than frames per second, where your pc components would matter
    {
        //we tell the player motor to move using the value from the movement action
        playerMotor.ProcessMove(onFootActions.Movement.ReadValue<Vector2>());
    }
    private void LateUpdate()
    {
        playerLook.ProcessLook(onFootActions.LookAround.ReadValue<Vector2>());
    }
    private void OnEnable()
    {
        onFootActions.Enable();
    }
    private void OnDisable()
    {
        onFootActions.Disable();
    }
}
