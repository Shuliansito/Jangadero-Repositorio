using UnityEngine;
using UnityEngine.InputSystem;

public class CamRaycast : MonoBehaviour
{
    [SerializeField] private LayerMask m_Interactable;
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject _dotCrosshair;
    [SerializeField] private GameObject _interactCrosshair;
    public InputActionReference Interact;
    private Ray ray;

    private void Update()
    {

        ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        Debug.DrawRay(ray.origin, ray.direction * 1.5f, Color.red);

        if (Physics.Raycast(ray, out RaycastHit hit, 1.5f, m_Interactable))
        {
            ChangeCrossHair(true);
            Debug.Log("Interacting");
            if (Interact.action.WasPressedThisFrame())
            {
               IInteracrable I_Interact = hit.collider.GetComponent<IInteracrable>();
                if (I_Interact != null)
                {
                    I_Interact.WasInteracted();
                }
                
                Debug.Log("Interact");

            }
        }
        else { ChangeCrossHair(false); }

    }

    private void ChangeCrossHair(bool canInteract)
    {
        _dotCrosshair.SetActive(!canInteract);
        _interactCrosshair.SetActive(canInteract);
    }
}
