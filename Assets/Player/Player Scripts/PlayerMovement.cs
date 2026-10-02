using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.IO.LowLevel.Unsafe;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    public PlayerClass playerClass = new(7f, 100f);
    public InputActionReference playerMovement;
    public InputActionReference playerRun;
    [SerializeField] public bool isRunning;
    private float movement;
    [SerializeField]
    private GameObject rb_player;

    [SerializeField]
    private float rotationSpeed;
    [SerializeField]
    [Range(0f,50f)]
    private float movementSpeed;


    

    void Awake()
    {


    }



    private void Update()
    {
        movement = playerMovement.action.ReadValue<float>();
        PlayerAccelerate(1.5f);

        transform.position += transform.forward * movementSpeed * Time.deltaTime;

        Debug.Log(playerMovement.action.enabled);
        Debug.Log(movement);
    }
    void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        if (movement < 0) { transform.Rotate(0, -rotationSpeed * Time.deltaTime, 0); }
        else if (movement > 0) transform.Rotate(0, +rotationSpeed * Time.deltaTime, 0);
        

    }
    private void PlayerAccelerate(float speed)
    {
        Debug.Log("Detecting acceleration");
        
        if (playerRun.action.WasPressedThisFrame())
        {
            movementSpeed*=speed;
            Debug.Log("Running");
            isRunning = true;
        }
        else { isRunning = false; movementSpeed=20;}
    }





}
