using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject _selector;
    public GameObject _mainMenuButtons;
    public GameObject _settingsMenu;
    public GameObject _levelsMenu;
    public GameObject _controllersMenu;

    [Header("Settings Sliders")]
    public Slider _mouseSense;
    public Slider _joystickSense;

    private string configPath;

    private void Start()
    {
        if (Time.timeScale == 0.0f)
        {
            Time.timeScale = 1.0f;
        }

        InputSystem.ResetHaptics();

        configPath = Path.Combine(Application.persistentDataPath, "Sensitivity.txt");

        LoadSettings();


        Debug.Log(configPath);
    }

    public void OnStartPressed()
    {
        SaveSettings();
        SceneManager.LoadScene(sceneName: "Nivel1");
    }

    public void OnControllersEnter() 
    {
        _settingsMenu.SetActive(false);
        _controllersMenu.SetActive(true);
    }
    public void OnControllersExit() 
    {
        _settingsMenu.SetActive(true);
        _controllersMenu.SetActive(false);
    }
    public void OnExitPressed()
    {
        SaveSettings();
        Application.Quit();
    }

    public void OnSettingsPressed()
    {
        _mainMenuButtons.SetActive(false);
        _settingsMenu.SetActive(true);
    }

    public void OnExitSettings()
    {
        SaveSettings();

        _settingsMenu.SetActive(false);
        _controllersMenu.SetActive(false);
        _mainMenuButtons.SetActive(true);
    }

    public void OnLevelSelectorExit()
    {
        _levelsMenu.SetActive(false);
        _mainMenuButtons.SetActive(true);
    }

    public void OnLevelSelectorEnter()
    {
        _mainMenuButtons.SetActive(false);
        _levelsMenu.SetActive(true);
    }

    private void LoadSettings()
    {
        if (File.Exists(configPath))
        {
            string[] settings = File.ReadAllLines(configPath);

            _mouseSense.value = float.Parse(settings[0]);
            _joystickSense.value = float.Parse(settings[1]);
        }
        else
        {
            _mouseSense.value = 0.1f;
            _joystickSense.value = 2f;

            SaveSettings();
        }
    }

    public void SaveSettings()
    {
        string datos = _mouseSense.value + "\n" + _joystickSense.value;

        File.WriteAllText(configPath, datos);
    }
}