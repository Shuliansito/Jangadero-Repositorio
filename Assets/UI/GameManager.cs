using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{
    [SerializeField] private InputActionReference playerMovement;
    [SerializeField] private InputActionReference playerRun;
    [SerializeField] private InputActionReference playerJump;
    [SerializeField] private InputActionReference playerPause;

    private string configPath;

    private void Awake()
    {
        InputSystem.settings.maxEventBytesPerUpdate = 0;
        playerMovement.action.Enable();
        playerRun.action.Enable();
        playerJump.action.Enable();
        playerPause.action.Enable();

    }


    public void StartGame(int level)
    {
       

    }
}
