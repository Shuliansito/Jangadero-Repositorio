using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{
    [SerializeField] private InputActionReference playerMovement;
    [SerializeField] private InputActionReference playerRun;
    [SerializeField] private InputActionReference playerJump;



    private void Awake()
    {
        InputSystem.settings.maxEventBytesPerUpdate = 0;
        playerMovement.action.Enable();
        playerRun.action.Enable();
        playerJump.action.Enable();

    }
    private void Start()
    {

    }
    private void Update()
    {
        

    }


    public void StartGame(int level)
    {
       

    }
}
