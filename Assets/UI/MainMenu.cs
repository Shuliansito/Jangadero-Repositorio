using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject _selector;
    public GameObject _mainMenuButtons;
    public GameObject _settingsMenu;


    private void Start()
    {
        if (Time.timeScale == 0.0f)
        {
            Time.timeScale = 1.0f;
        }
        InputSystem.ResetHaptics();
    }

    public void OnStartPressed()
    {
        SceneManager.LoadScene(sceneName: "Nivel1");
    }
    public void OnExitPressed()
    {
        Application.Quit();
    }

    public void OnSettingsPressed()
    {
        _mainMenuButtons.SetActive(false);
        _settingsMenu.SetActive(true);
    }

    public void OnExitSettings()
    {
        _settingsMenu.SetActive(false);
        _mainMenuButtons.SetActive(true);
    }
}
