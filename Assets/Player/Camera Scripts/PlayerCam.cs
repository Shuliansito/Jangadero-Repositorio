using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity;
    public float stickSensitivity;
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
            Vector2 joystickLook = Gamepad.current != null ? Gamepad.current.rightStick.ReadValue() : Vector2.zero;

            mouseX = mouseLook.x * mouseSensitivity;
            mouseY = mouseLook.y * mouseSensitivity;

            mouseX += joystickLook.x * stickSensitivity;
            mouseY += joystickLook.y * stickSensitivity;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -45f, 45f);

            yRotation += mouseX;
            yRotation = Mathf.Clamp(yRotation, -45f, 45f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.localRotation = Quaternion.Euler(0f, yRotation, 0f);
        }
    }
}
