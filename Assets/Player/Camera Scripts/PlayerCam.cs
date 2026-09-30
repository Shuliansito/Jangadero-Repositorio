using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 0.1f;
    public float stickSensitivity = 100f;

    private float xRotation = 0f;
    private Transform playerBody;

    private void Awake()
    {
        playerBody = transform.root;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void LateUpdate()
    {
        if (Time.timeScale != 0.0f)
        {
            // Mouse
            Vector2 mouseLook = Mouse.current != null ? Mouse.current.delta.ReadValue() : Vector2.zero;
            float mouseX = mouseLook.x * mouseSensitivity;
            float mouseY = mouseLook.y * mouseSensitivity;

            // Joystick
            Vector2 stickLook = Vector2.zero;
            if (Gamepad.current != null)
                stickLook = Gamepad.current.rightStick.ReadValue();

            float stickX = stickLook.x * stickSensitivity * Time.deltaTime;
            float stickY = stickLook.y * stickSensitivity * Time.deltaTime;

            // Combinar
            float totalX = mouseX + stickX;
            float totalY = mouseY + stickY;
            

            xRotation -= totalY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * totalX);
        }
    }
}