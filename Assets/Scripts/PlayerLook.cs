using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//HERE IS THE CAMERA MOVEMENT FOR THE FIRST PERSON VIEW OF THE PLAYER
public class PlayerLook : MonoBehaviour
{
    public Camera cam;
    public float xRotation = 0f;
    public float ySensitivity = 20f;
    public float xSensitivity = 20f;
    
    public void ProcessLook(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;
        //we calculate camera rotation for looking up and down
        xRotation -= (mouseY * Time.deltaTime) * ySensitivity;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f); //clamp states that xrotation has values from min to max

        //we apply to our camera transform
        //q.e returns a rotation that rotates x degrees around the x axis, z around z axis and y around y
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        
        //we rotate player to look left and right
        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSensitivity);
    }
}
