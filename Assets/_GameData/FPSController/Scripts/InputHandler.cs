using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private PlayerInputs _PlayerInputs;
    private bool _IsRunning;

    private void Awake()
    {
        _PlayerInputs = new();
    }

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked;

        _PlayerInputs.Enable();
        _PlayerInputs.Gameplay.Enable();

        _PlayerInputs.Gameplay.Running.performed += ctx => _IsRunning = true;
        _PlayerInputs.Gameplay.Running.canceled += ctx => _IsRunning = false;
    }

    private void OnDisable()
    {
        _PlayerInputs.Disable();
        _PlayerInputs.Gameplay.Disable();
    }

    public Vector2 GetMovementInput() => _PlayerInputs.Gameplay.Movement.ReadValue<Vector2>();
    public Vector2 GetMouseInput() => _PlayerInputs.Gameplay.MouseDelta.ReadValue<Vector2>();
    public bool GetJumpInput() => _PlayerInputs.Gameplay.Jump.triggered;
    public bool GetRunningInput() => _IsRunning;
}
