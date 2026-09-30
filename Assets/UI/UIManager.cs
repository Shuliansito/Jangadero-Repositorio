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

        //Pause menu check
        if (_pauseMenu.action.WasPressedThisFrame())
        {
            Debug.Log("Escape/Home apretado");
            UpdateCursorState();
            pauseUI.SetActive(_isPaused);
        }

        //Value of Health and Stamina sliders depending on player stats
        _staminaSlider.value = _player.PlayerStamina;
        _healthSlider.value = _player.PlayerHealth;

        #region Stamina
        //Check if stamina changed
        if (_player.PlayerStamina != _lastStamina)
        {
            //Debug.Log("Desigualdad Stamina: "+_player.PlayerStamina+" /-/ "+_lastStamina);
            StaminaTimer = 2.0f;
            StaminaSliderGO.SetActive(true);
        }

        //Decrease stamina timer
        if (StaminaTimer > 0)
        {
            StaminaTimer -= Time.deltaTime;
        }
        else
        {
            StaminaSliderGO.SetActive(false);
        }

        
        _lastStamina = _player.PlayerStamina;

        #endregion
        if (_player.PlayerHealth != _lastHealth)
        {
           //Debug.Log("Desigualdad Vida: " + _player.PlayerHealth + " /-/ " + _lastHealth); 
            HealthTimer = 5.0f;
            HealthSliderGO.SetActive(true);
        }
        
        if (HealthTimer > 0)
        {
            HealthTimer -= Time.deltaTime;
        }
        else
        {
            HealthSliderGO.SetActive(false);
        }
        _lastHealth = _player.PlayerHealth;


        RectTransform staminaRect = StaminaSliderGO.GetComponent<RectTransform>();

        if (HealthSliderGO.activeSelf)
        {
            staminaRect.anchoredPosition = new Vector2(
                staminaRect.anchoredPosition.x,
                -60
            );
        }
        else
        {
            staminaRect.anchoredPosition = new Vector2(
                staminaRect.anchoredPosition.x,
                0
            );
        }
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