using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float HorizontalSensitivity;

    private InputHandler _InputHandler;
    private MotorHandler _MotorHandler;

    private void Awake()
    {
        _InputHandler = GetComponent<InputHandler>();
        _MotorHandler = GetComponent<MotorHandler>();
    }

    private void Update()
    {
        ApplyCharacterMovement();
        ApplyCharacterJump();
        ApplyCharacterRunning();
    }

    private void ApplyCharacterMovement() => _MotorHandler.moveDir = _InputHandler.GetMovementInput();
    private void ApplyCharacterJump() => _MotorHandler.hasJumped = _InputHandler.GetJumpInput();
    private void ApplyCharacterRunning() => _MotorHandler.isRunning = _InputHandler.GetRunningInput();
}
