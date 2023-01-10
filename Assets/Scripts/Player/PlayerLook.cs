using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public new Camera camera;
    private float xRotation = 0f;
    
    public float xSensitivity = 30f;
    public float ySensitivity = 30f;

    private void Start() 
    {
        //locks the cursor inside the game screen, click on the screen to lock and ESC to unlock
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void ProcessLook(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;

        //calculate the cam rotation for look up and down
        xRotation -= (mouseY * Time.deltaTime) * ySensitivity;
        //those 80f are the limitation of the turning of the camera we could also use 90 degrees instead
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);
        
        //apply to camera transform
        camera.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        //rotate player view left and right
        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSensitivity);
    }
}
