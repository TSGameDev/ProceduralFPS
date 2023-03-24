using UnityEngine;

namespace TSGameDev.FPS.Movement
{
    [RequireComponent(typeof(MotorHandlerCC))]
    [RequireComponent(typeof(RotationHandlerCC))]
    [RequireComponent(typeof(InputHandler))]
    public class PlayerControllerCC : MonoBehaviour
    {
        private InputHandler _InputHandler;
        private MotorHandlerCC _MotorHandler;
        private RotationHandlerCC _RotationHandler;

        private void Awake()
        {
            _InputHandler = GetComponent<InputHandler>();
            _MotorHandler = GetComponent<MotorHandlerCC>();
            _RotationHandler = GetComponent<RotationHandlerCC>();
        }

        private void Update()
        {
            ApplyCharacterMovement();
            ApplyCharacterJump();
            ApplyCharacterRunning();
            ApplyCharacterRotation();
        }

        private void ApplyCharacterMovement() => _MotorHandler.moveDir = _InputHandler.GetMovementInput();
        private void ApplyCharacterRotation() => _RotationHandler.mouseInput= _InputHandler.GetMouseInput();
        private void ApplyCharacterJump() => _MotorHandler.hasJumped = _InputHandler.GetJumpInput();
        private void ApplyCharacterRunning() => _MotorHandler.isRunning = _InputHandler.GetRunningInput();
    }
}
