using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    [Header ("UI and Audio")]
    [SerializeField] private GameObject _pauseMenuButtons;
    [SerializeField] private GameObject _settingsMenu;
    [SerializeField] private GameObject _winCanva;
    [SerializeField] private GameObject _audioManager;
    private string configPath;

    private string playerDataPath;
    [Space(5)]
    [Header ("Camera controll")]
    [SerializeField] private MouseLook camControll;

    [Space(5)]
    [Header("Pause Menu inputs")]
    [SerializeField] private InputActionReference pauseMenuButton;
     public bool isOnPauseMenu;
   
    [Space(5)]
    [Header("Sensitivity")]
    public float _mouseSensitivity;
    public float _joystickSensitivity;

    [Space(5)]
    [Header("Settings Sliders")]
    public Slider _mouseSense;
    public Slider _joystickSense;

    private void Start()
    {
        configPath = Path.Combine(Application.persistentDataPath, "Sensitivity.txt");
        playerDataPath = Path.Combine(Application.persistentDataPath, "PlayerData.txt");

        LoadSettings();
    }
    private void LoadSettings()
    {
        if (File.Exists(configPath))
        {
            string[] settings = File.ReadAllLines(configPath);

            _mouseSensitivity = float.Parse(settings[0]);
            _joystickSensitivity = float.Parse(settings[1]);

            camControll.mouseSensitivity = _mouseSensitivity;
            camControll.stickSensitivity = _joystickSensitivity;
        }
        else
        {
            _mouseSensitivity = 0.1f;
            _joystickSensitivity = 2f;

            camControll.mouseSensitivity = _mouseSensitivity;
            camControll.stickSensitivity = _joystickSensitivity;
        }
    }

    public void PlayerWin(int levelCompleted)
    {
        _winCanva.SetActive(true);
        pauseMenuButton.action.Disable();
        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        File.WriteAllText(playerDataPath, levelCompleted.ToString() );
    }

    // Update is called once per frame
    void Update()
    {
        if (pauseMenuButton.action.WasPressedThisFrame())
        {
            ChangePauseState();
        }

        _audioManager.SetActive(Time.timeScale>0);

    }

    private void ChangePauseState()
    {
        isOnPauseMenu = !isOnPauseMenu;
        _pauseMenuButtons.SetActive(isOnPauseMenu);

        Time.timeScale = isOnPauseMenu ? 0f : 1f;

        Cursor.lockState = isOnPauseMenu ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = isOnPauseMenu;
    }

    public void OnResume()
    {
        ChangePauseState();
    }



    public void OnSettingsOpen()
    {
        _settingsMenu.SetActive(true);
    }
    public void OnBacktoMenu()
    {
        SceneManager.LoadScene(sceneName: "Main Menu");
    }

    public void SaveSettings()
    {
        string datos = _mouseSense.value + "\n" + _joystickSense.value;

        File.WriteAllText(configPath, datos);

        _settingsMenu.SetActive(false);
        LoadSettings();
    }
}
