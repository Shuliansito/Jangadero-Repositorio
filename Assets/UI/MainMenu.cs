using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject _selector;
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
}
