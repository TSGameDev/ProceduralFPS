using UnityEngine;

namespace TSGameDev.FPS.Movement
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float HorizontalSensitivity;

        private InputHandler _InputHandler;
        private MotorHandlerCC _MotorHandler;

        private void Awake()
        {
            _InputHandler = GetComponent<InputHandler>();
            _MotorHandler = GetComponent<MotorHandlerCC>();
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
}
