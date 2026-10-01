using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{
    [SerializeField] private InputActionReference playerMovement;



    private void Awake()
    {
        InputSystem.settings.maxEventBytesPerUpdate = 0;
        playerMovement.action.Enable();

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
