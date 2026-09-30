using System;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public PlayerClass playerClass = new(7f, 100f);

    [Header("Player Stats")]
    public Rigidbody rb;
    public float PlayerStamina;
    public float PlayerHealth;
    
    

    [Header ("Movement")]
    [Tooltip("Direction where the player is moving")]
    [SerializeField] private Vector2 _movingDirection;


    [Tooltip("Movement speed")]
    [Range(0f, 20f)]
    [SerializeField] private float _movingSpeed;


    [Tooltip("Detects if player is moving")]
    [SerializeField] private float crouchTimer=0;

    [Tooltip("Detects if player is moving")]
    [SerializeField] private bool _ismoving;

    [Tooltip("Detects if player is crouching")]
    [SerializeField] private bool _iscrouching;


    [Tooltip("Detects if player is sprinting")]
    [SerializeField] private bool _sprinting;


    [Tooltip("Move input action")]
    public InputActionReference Move;

    [Tooltip("Crouch input action")]
    public InputActionReference Crouch;


    [Tooltip("Run input action")]
    public InputActionReference Run;



    

    [Header("UI References")]

    public GameObject StaminaSlider;
    public GameObject HealthSlider;
    public Camera cam;
    public GameObject _dotCrosshair;
    public GameObject _interactCrosshair;

    [Header("Place Holders")]
    [SerializeField] private bool _interacting;
    public InputActionReference Hurt;
    public InputActionReference Healing;


    void Awake()
    {

        PlayerStamina = playerClass.GetStamina();
        PlayerHealth = playerClass.GetHealth();

    }

    private void Update()
    {

        //
        //  FIXED Error 13: When player was in pause menu, game logic was still running,
        //  so clicking the running input made the FOV change when that shouldn't happen    
        //                                                                                  
        //  Solution: Having a condition that if time scale <= 0, skips all game logic       
        //
        if (Time.timeScale <= 0) { return; }

        playerClass.HealthLimit();
        
        
        UpdatePlayerStats();
        MovementLogic();
        CrouchLogic();
        RunningLogic();

        
        if (Hurt.action.WasPressedThisFrame()) { playerClass.Hurt(10f); }
        if (Healing.action.WasPressedThisFrame()) { playerClass.Healing(10f); }


    }
    void FixedUpdate()
    {
        MovePlayer();
    }


    /// <summary>
    /// Updates the player's velocity based on the current movement direction and speed.
    /// </summary>
    /// <remarks>This method applies movement to the player by setting the Rigidbody's velocity according to
    /// the movement direction and speed. It should be called within the physics update loop (e.g., FixedUpdate) to
    /// ensure consistent movement behavior. The vertical velocity is preserved, allowing for gravity and
    /// jumping to function as expected.</remarks>
    private void MovePlayer()
    {
        Vector3 direction =
            transform.right * _movingDirection.x +
            transform.forward * _movingDirection.y;

        rb.velocity = new Vector3(
            direction.x * _movingSpeed,
            rb.velocity.y,
            direction.z * _movingSpeed
        );
    }
    /// <summary>
    /// Updates the player's health and stamina values based on the current state of the player class.
    /// </summary>
    private void UpdatePlayerStats()
    {
        PlayerHealth = playerClass.GetHealth();
        PlayerStamina = playerClass.GetStamina();
    }
    /// <summary>
    /// Handles the player's crouch state, toggling between crouching and standing based on input and cooldown timer.
    /// </summary>
    /// <remarks>This method adjusts the player's scale and movement speed when crouching or standing. It
    /// enforces a cooldown period to prevent rapid toggling between states. Intended to be called within the player's
    /// update loop to process crouch input each frame.</remarks>
    private void CrouchLogic()
    {
        if (Crouch.action.WasPressedThisFrame() && crouchTimer <= 0|| Crouch.action.WasPressedThisFrame() && _iscrouching)
        {
            _iscrouching = !_iscrouching;

            if (_iscrouching)
            {
                transform.localScale = new Vector3(transform.localScale.x, 1.0f, transform.localScale.z);

                transform.localPosition=new Vector3(transform.localPosition.x, transform.localPosition.y-0.8f, transform.localPosition.z);   
                playerClass.SetSpeed((int)(playerClass.GetSpeed() / 2));
            }
            else
            {
                transform.localScale = new Vector3(transform.localScale.x, 1.8f, transform.localScale.z);
                playerClass.SetSpeed((int)(playerClass.GetSpeed() * 2));
            }

            crouchTimer = 10.0f;
        }

        crouchTimer -= 10.0f * Time.deltaTime;
        crouchTimer = Mathf.Clamp(crouchTimer, 0.0f, 10.0f);
    }


    /// <summary>
    /// Handle player running logic, changing the FOV and player speed when performing the action. 
    /// </summary>
    /// <remarks>While Run input is pressed, player ain't moving and stamina is more than 0.5, player speed is boosted by 'runBoost' value, running boolean and stamina bar are
    /// changed. Finally, Stamina is decreased by staminaWaste.</remarks>
    private void RunningLogic()
    {
        float runBoost=1.5f;
        float staminaWaste=20f;

        if (Run.action.IsPressed() && _ismoving && PlayerStamina > 0.5)
        {
            cam.fieldOfView = 63;
            _sprinting = true;
            _movingSpeed = playerClass.GetSpeed() * runBoost;
        }
        else
        {
            cam.fieldOfView = 60;
            _sprinting = false;
            _movingSpeed = playerClass.GetSpeed();
        }

        if (_sprinting)
        {
            playerClass.UpdateStamina(-10.0f * Time.deltaTime);
            StaminaSlider.SetActive(true);
        }

        if (!_sprinting && !_ismoving)
        {
            playerClass.UpdateStamina(staminaWaste * Time.deltaTime);
        }
    }
    /// <summary>
    /// Processes player movement input from both keyboard and gamepad, updating the current movement direction and
    /// movement state.
    /// </summary>
    /// <remarks>This method combines input from the keyboard and the left stick of the current gamepad, if
    /// available. The resulting movement direction is clamped to a maximum magnitude of 1 to ensure consistent movement
    /// speed regardless of input strength. Intended to be called as part of the player movement update cycle.</remarks>
    private void MovementLogic()
    {
        Vector2 keyboardInput = Move.action.ReadValue<Vector2>();

        Vector2 stickInput = Vector2.zero;
        if (Gamepad.current != null)
            stickInput = Gamepad.current.leftStick.ReadValue();
        
        _movingDirection = keyboardInput + stickInput;
        _movingDirection = Vector2.ClampMagnitude(_movingDirection, 1f);

        _ismoving = _movingDirection.x != 0 || _movingDirection.y != 0;
    }



}
