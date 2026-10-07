using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private PlayerMovement mMovement;
    [SerializeField] private GameUIManager UIManager;

    private void Start()
    {
        mMovement = GetComponentInParent<PlayerMovement>();
        if (mMovement == null) Debug.LogWarning("mMovement not found");
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            mMovement.PlayerReset();
        }
        if (collision.gameObject.CompareTag("Dock Level 1"))
        {
            UIManager.PlayerWin(1);
        }
    }
}
