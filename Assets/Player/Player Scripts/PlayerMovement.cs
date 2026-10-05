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
    #region Variables
    [SerializeField]
    private GameObject rb_player;

    [Header("Movimiento Jugador")]
    [SerializeField]
    private InputActionReference playerMovement;

    [SerializeField]
    private InputActionReference playerRun;

    public bool isRunning;

    private float movement;

    [Tooltip("Bool que decide si puede o no moverse [SE PUEDE CAMBIAR DESDE CUALQUIER SCRIPT]")]

    public bool canMove;

    [SerializeField]
    private float previusRotation;

    [SerializeField]
    [Range(0f, 75f)]
    private float rotationSpeed;

    [Range(0f, 50f)]
    public float movementSpeed;

    [SerializeField]
    [Range(0f, 75f)]
    float newRotation = 0;

    [Range(0f, 200f)]
    public float movementDrift;

    [Range(0f, 100f)]
    public float movementDrag;


    [Header("Camara")]

    [SerializeField]

    private Camera cam;

    #endregion

    private void Update()
    {
        movement = playerMovement.action.ReadValue<float>();
        PlayerAccelerate(5f);
        transform.position += transform.forward * movementSpeed * Time.deltaTime;
    }
    void FixedUpdate()
    {
        MovePlayer();
    }

    
    private void MovePlayer()
    {
        MovementLogic();

        //TODO: Sacar estos limites y hacer un sistema mejorado
        #region LIMITES LATERALES
        float angle = Mathf.DeltaAngle(0f, transform.eulerAngles.y);
        angle = Mathf.Clamp(angle, -90f, 90f);

        Vector3 euler = transform.eulerAngles;
        euler.y = angle;

        transform.rotation = Quaternion.Euler(euler);
        #endregion
    }


    private void MovementLogic()
    {
        if (!canMove) return;

        if (movement < 0)
        {
            rotationSpeed += movementDrag * Time.deltaTime;
            rotationSpeed = Mathf.Clamp(rotationSpeed, 0, 75);
            newRotation = -rotationSpeed;
            previusRotation = -1;
        }
        else if (movement > 0)
        {
            rotationSpeed += movementDrag * Time.deltaTime;
            rotationSpeed = Mathf.Clamp(rotationSpeed, 0, 75);
            newRotation = rotationSpeed;
            previusRotation = 1;
        }
        else
        {
            rotationSpeed -= movementDrift * Time.deltaTime;
            rotationSpeed = Mathf.Clamp(rotationSpeed, 0, 75);

            if (previusRotation == -1) newRotation = -rotationSpeed;
            if (previusRotation == 1) newRotation = rotationSpeed;
        }
        transform.Rotate(0, newRotation * Time.deltaTime, 0);
    }
    private void PlayerAccelerate(float speed)
    {

        float FOVSmoothness = 5f;


        float newCameraField;
        //TODO: Mejorar el cambio de FOV al acelerar, algo mas dinamico
        if (playerRun.action.IsPressed())
        {
            this.movementSpeed = 25f;


            isRunning = true;
            //Movimiento suave de camara
            newCameraField = cam.fieldOfView += FOVSmoothness * Time.deltaTime;
            newCameraField = Mathf.Clamp(newCameraField, 70, 75);
            cam.fieldOfView = newCameraField;

        }
        else
        {
            this.movementSpeed = 20f;
            isRunning = false;
            newCameraField = cam.fieldOfView -= FOVSmoothness * Time.deltaTime;
            newCameraField = Mathf.Clamp(newCameraField, 70, 75);

            cam.fieldOfView = newCameraField;
        }


    }

}
