using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using ColorUtility = UnityEngine.ColorUtility;

public class UIManager : MonoBehaviour
{
    public InputActionReference _pauseMenu;
    private bool _isPaused;
    public GameObject pauseUI;
    public GameObject HealthSliderGO;
    public GameObject StaminaSliderGO;
    public PlayerMovement _player;
    [SerializeField] private Image _staminaFill;
    public Slider _staminaSlider;
    public Slider _healthSlider;


    [SerializeField] private float StaminaTimer=-1;
    [SerializeField] private float _lastStamina;
    [SerializeField] private float HealthTimer=-1;
    [SerializeField] private float _lastHealth;
    [SerializeField] private InputActionAsset _inputActions;

    private void OnEnable()
    {
        _inputActions.FindActionMap("UI").Enable();
        _inputActions.FindActionMap("Pause Menu").Enable();
    }
    private void Start()
    {
       // Debug.Log("Start");
        _lastHealth=_player.playerClass.GetHealth();
       // Debug.Log("Ultima Vida: "+_lastHealth);
        _lastStamina=_player.playerClass.GetStamina();
        //Debug.Log("Ultima Vida: " + _lastStamina);

       // Debug.Log("Termina el Start");
    }
    private void Update()
    {


    }

    private void UpdateCursorState()
    {
        _isPaused = !_isPaused;
        Cursor.lockState = _isPaused ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = _isPaused;
        Time.timeScale = _isPaused ? 0.0f : 1.0f;
        if (_isPaused)
        {
            UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
        }
    }

    public void OnResume()
    {
        _isPaused = false;
        pauseUI.SetActive(false);
        Time.timeScale = 1.0f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void OnExit()
    {
        SceneManager.LoadScene(sceneName: "Main Menu");
    }
}