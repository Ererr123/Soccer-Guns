using UnityEngine.InputSystem;
using Cinemachine;
using UnityEngine;

public class SwitchVcam : MonoBehaviour
{
    [SerializeField]
    private PlayerInput PlayerInput;

    [SerializeField]
    private int priority = 10;

    [SerializeField]
    private Canvas aimCanvas;

    // Start is called before the first frame update
    private InputAction Aim;

    private CinemachineVirtualCamera VirtualCamera;
    private void Awake()
    {
        aimCanvas.enabled = false;
        VirtualCamera = GetComponent<CinemachineVirtualCamera>();
        Aim = PlayerInput.actions["Aim"];
    }

    private void OnEnable()
    {
        Aim.performed += _ => StartAim();
        Aim.canceled += _ => CancelAim();
    }

    private void OnDisable()
    {
        Aim.performed -= _ => StartAim();
        Aim.canceled -= _ => CancelAim();
    }

    private void StartAim()
    {
        VirtualCamera.Priority += priority;
        aimCanvas.enabled = true;
    }

    private void CancelAim()
    {
        VirtualCamera.Priority -= priority;
        aimCanvas.enabled = false;
    }
}
