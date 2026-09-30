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
    private InputActionReference playerMovement;
    private float movement;

    

    void Awake()
    {

        

    }

    private void Update()
    {
        
    }
    void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {

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
