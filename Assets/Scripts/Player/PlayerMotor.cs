using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//HERE YOU CONTROL ALL THE PLAYER MOVEMENT FUNCTIONALITY
public class PlayerMotor : MonoBehaviour
{
    private CharacterController characterController;
    private Vector3 playerVelocity;
    private bool isGrounded;
    private bool lerpCrouch = false;
    private bool crouching = false;
    private bool walking = false;
    private float crouchTimer = 0f;
    public float playerSpeed = 5f;
    public float gravity = -9.8f;
    public float jumpHeight = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = characterController.isGrounded;
        if (lerpCrouch)
        {
            crouchTimer += Time.deltaTime * 4;
            float p = crouchTimer / 1;
            p *= p;
            if (crouching)
            {
                characterController.height = Mathf.Lerp(characterController.height, 1, p);
            }
            else characterController.height = Mathf.Lerp(characterController.height, 2, p);

            if (p > 1)
            {
                lerpCrouch = false;
                crouchTimer = 0f;
            }
        }
    }
    
    //we receive the inputs from InputManager and assign them to character controller 
    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        characterController.Move(transform.TransformDirection(moveDirection) * playerSpeed * Time.deltaTime); //constant in directii
        //we apply a constant downward force toward the player
        playerVelocity.y += gravity * Time.deltaTime;
        if (isGrounded && playerVelocity.y<0) {
            playerVelocity.y = -2f;
        }
        characterController.Move(playerVelocity * Time.deltaTime); //constant in jos
    }

    public void Jump()
    {
        if (isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    public void Crouch()
    {
        crouching = !crouching;
        crouchTimer = 0;
        lerpCrouch = true;
    }

    public void Stealth()
    {
        walking = !walking;
        if(walking) {
            playerSpeed = 2;
        }
        else
        {
            playerSpeed = 5;
        }
    }
}
