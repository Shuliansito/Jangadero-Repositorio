using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour
{
    [SerializeField]
    private InputActionReference playerJump;

    [SerializeField]
    private Transform detectapiso;

    [SerializeField]
    private float distanciapiso = 0.2f;

    [SerializeField]
    private LayerMask mascarapiso;

    [SerializeField]
    private float salto = 5f;

    [SerializeField]
    private bool isJumping;

    [SerializeField]
    private bool isOnGround;

    public bool canJump = true;

    private Rigidbody rb_player;


    private void Start()
    {
        rb_player = GetComponent<Rigidbody>();
    }


    private void Update()
    {
        isOnGround = Physics.CheckSphere(
            detectapiso.position,
            distanciapiso,
            mascarapiso
        );

        if (playerJump.action.WasPressedThisFrame())
        {
            if (isOnGround && canJump)
            {
                isJumping = true;
            }
        }
    }


    private void FixedUpdate()
    {
        if (isJumping)
        {
            rb_player.AddForce(
                Vector3.up * salto,
                ForceMode.Impulse
            );

            isJumping = false;
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        if (detectapiso != null)
        {
            Gizmos.DrawWireSphere(
                detectapiso.position,
                distanciapiso
            );
        }
    }
}