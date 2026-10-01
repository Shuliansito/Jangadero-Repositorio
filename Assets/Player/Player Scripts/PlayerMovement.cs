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
        if (movement < 0) { transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y - rotationSpeed * Time.deltaTime, transform.rotation.z); }
        else if (movement > 0) transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y + rotationSpeed*Time.deltaTime, transform.rotation.z);
        else { transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z); }

    }

    private void UpdatePlayerStats()
    {

    }

    private void CrouchLogic()
    {

    }

    private void RunningLogic()
    {

    }

    private void MovementLogic()
    {

    }



}
