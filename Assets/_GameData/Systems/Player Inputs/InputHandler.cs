using UnityEngine;

namespace TSGameDev.FPS
{
    public class InputHandler : MonoBehaviour
    {
        private PlayerInputs _PlayerInputs;
        private bool _IsRunning;
        private bool _IsLeftMouseHeld;
        private bool _IsRightMouseHeld;

        private void Awake()
        {
            _PlayerInputs = new();
        }

        private void OnEnable()
        {
            Cursor.lockState = CursorLockMode.Locked;

            _PlayerInputs.Enable();
            _PlayerInputs.Gameplay.Enable();

            _PlayerInputs.Gameplay.Shift.performed += ctx => _IsRunning = true;
            _PlayerInputs.Gameplay.Shift.canceled += ctx => _IsRunning = false;

            _PlayerInputs.Gameplay.LeftClick.performed += ctx => _IsLeftMouseHeld = true;
            _PlayerInputs.Gameplay.LeftClick.canceled += ctx => _IsLeftMouseHeld = false;

            _PlayerInputs.Gameplay.RightClick.performed += ctx => _IsRightMouseHeld = true;
            _PlayerInputs.Gameplay.RightClick.canceled += ctx => _IsRightMouseHeld = false;
        }

        private void OnDisable()
        {
            _PlayerInputs.Disable();
            _PlayerInputs.Gameplay.Disable();
        }

        public Vector2 GetMovementInput() => _PlayerInputs.Gameplay.WASD.ReadValue<Vector2>();
        public Vector2 GetMouseInput() => _PlayerInputs.Gameplay.MouseDelta.ReadValue<Vector2>();
        public bool GetJumpInput() => _PlayerInputs.Gameplay.Space.triggered;
        public bool GetLeftClick() => _PlayerInputs.Gameplay.LeftClick.triggered;
        public bool GetLeftClickHeld() => _IsLeftMouseHeld;
        public bool GetRightClick() => _PlayerInputs.Gameplay.RightClick.triggered;
        public bool GetRightClickHeld() => _IsRightMouseHeld;
        public bool GetRunningInput() => _IsRunning;
    }
}
