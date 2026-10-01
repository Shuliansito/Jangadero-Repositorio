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
    public InputActionReference playerMovement;
    private float movement;
    [SerializeField]
    private GameObject rb_player;

    [SerializeField]
    private float rotationSpeed;
    [SerializeField]
    [Range(0f,20f)]
    private float movementSpeed;

    

    void Awake()
    {

         

    }

    private void Update()
    {
        movement = playerMovement.action.ReadValue<float>();
        transform.position=new Vector3(transform.position.x, transform.position.y, transform.position.z+movementSpeed*Time.deltaTime);

        Debug.Log(playerMovement.action.enabled);
        Debug.Log(movement);
    }
    void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        if (movement < 0) { transform.Rotate(0, rotationSpeed * Time.deltaTime, 0); }
        else if (movement > 0) transform.Rotate(0, -rotationSpeed * Time.deltaTime, 0);
        

    }





}
