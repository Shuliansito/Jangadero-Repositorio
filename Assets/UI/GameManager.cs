using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{
    [SerializeField] private InputActionReference playerMovement;
    [SerializeField] private InputActionReference playerRun;
    [SerializeField] private InputActionReference playerJump;
    [SerializeField] private MouseLook camControll;

    public float _mouseSensitivity;
    public float _joystickSensitivity;

    private string configPath;

    private void Awake()
    {
        InputSystem.settings.maxEventBytesPerUpdate = 0;
        playerMovement.action.Enable();
        playerRun.action.Enable();
        playerJump.action.Enable();

    }
    private void Start()
    {
        configPath = Path.Combine(Application.persistentDataPath, "Config.txt");

        LoadSettings();

        camControll.mouseSensitivity = _mouseSensitivity;
        camControll.stickSensitivity = _joystickSensitivity;
    }
    private void LoadSettings()
    {
        if (File.Exists(configPath))
        {
            string[] settings = File.ReadAllLines(configPath);

            _mouseSensitivity = float.Parse(settings[0]);
            _joystickSensitivity = float.Parse(settings[1]);
        }
        else
        {
            _mouseSensitivity = 0.1f;
            _joystickSensitivity = 2f;
        }
    }

    public void StartGame(int level)
    {
       

    }
}
