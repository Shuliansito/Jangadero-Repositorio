using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 0.1f;
    public float stickSensitivity = 100f;
    float mouseX;
    float mouseY;


    private float xRotation = 0f;
    private float yRotation = 0f;
    private Transform playerBody;

    private void Awake()
    {
        playerBody = transform.parent;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void LateUpdate()
    {
        if (Time.timeScale != 0.0f)
        {
         
            Vector2 mouseLook = Mouse.current != null ? Mouse.current.delta.ReadValue() : Vector2.zero;
            mouseX = mouseLook.x * mouseSensitivity;
            mouseY = mouseLook.y * mouseSensitivity;




            // Vertical
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -45f, 45f);

            // Horizontal
            yRotation += mouseX;
            yRotation = Mathf.Clamp(yRotation, -45f, 45f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.localRotation = Quaternion.Euler(0f, yRotation, 0f);
        }

    }
}