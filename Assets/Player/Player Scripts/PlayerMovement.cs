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
    private Camera cam;

    [SerializeField]
    private float rotationSpeed;
    [SerializeField]
    private float previusRotation;
    [Range(0f,50f)]
    public float movementSpeed;


    

    void Awake()
    {


    }
    


    private void Update()
    {

        //TODO: Limpiar el update moviendo estas lineas a procesos apartes.
        Debug.Log("Original: "+movementSpeed);
        movement = playerMovement.action.ReadValue<float>();
        PlayerAccelerate(5f);


        transform.position += transform.forward * movementSpeed * Time.deltaTime;


        Debug.Log(playerRun.action.enabled);
        Debug.Log(movementSpeed);
    }
    void FixedUpdate()
    {
        MovePlayer();
    }

    
    private void MovePlayer()
    {
        //TODO: Agregar smooth al movimiento lateral 


        MovementLogic();



        //TODO: Sacar estos limites y hacer un sistema mejorado
        float angle = Mathf.DeltaAngle(0f, transform.eulerAngles.y);
        angle = Mathf.Clamp(angle, -90f, 90f);

        Vector3 euler = transform.eulerAngles;
        euler.y = angle;

        transform.rotation = Quaternion.Euler(euler);
    }

    private void MovementLogic()
    {
        //Uso de esta variable para no repetir codigo (Visible en antiguos comits)
        float newRotation=0;

        if (movement < 0)
        {
            rotationSpeed = 75;
            newRotation = -rotationSpeed;
            previusRotation = -1;
        }
        else if (movement > 0)
        {
            rotationSpeed = 75;
            newRotation = +rotationSpeed;
            previusRotation = 1;
        }
        //Smooth movement
        else
        {   
            rotationSpeed -= 150 * Time.deltaTime;
            rotationSpeed = Mathf.Clamp(rotationSpeed, 0, 75);
            if (previusRotation == -1) newRotation = -rotationSpeed;
            if (previusRotation == 1) newRotation = +rotationSpeed;
        }
        transform.Rotate(0, newRotation * Time.deltaTime, 0);
    }

    private void PlayerAccelerate(float speed)
    {
        Debug.Log("Detecting acceleration");
        
        //TODO: Mejorar el cambio de FOV al acelerar, algo mas dinamico
        if (playerRun.action.IsPressed())
        {
            this.movementSpeed=25f;

            Debug.Log("New Movement Speed: "+ movementSpeed);
            isRunning = true;
            cam.fieldOfView = 75f;
        }
        else {this.movementSpeed = 20f; isRunning = false; cam.fieldOfView = 70f; }

    }





}
