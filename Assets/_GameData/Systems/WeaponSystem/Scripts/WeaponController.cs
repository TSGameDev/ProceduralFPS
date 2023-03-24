using TSGameDev.FPS.Movement;
using UnityEngine;

namespace TSGameDev.FPS.WeaponSystem
{
    public class WeaponController : MonoBehaviour
    {
        [SerializeField] private Weapon debugWeapon;

        private IWeapon _CurrentWeapon;
        private IMotor _CharacterMotor;
        private InputHandler _InputHandler;

        private void Awake()
        {
            _CurrentWeapon = debugWeapon;
            _InputHandler = GetComponent<InputHandler>();
            _CharacterMotor = GetComponent<IMotor>();
        }

        private void Update()
        {
            UpdateWeaponInputs();

            if (_InputHandler.GetLeftClick())
                ApplyWeaponFire();

            ApplyWeaponAim();
        }

        private void UpdateWeaponInputs()
        {
            _CurrentWeapon.MovementInput = _InputHandler.GetMovementInput();
            _CurrentWeapon.MouseInput = _InputHandler.GetMouseInput();
            _CurrentWeapon.IsRunning = _InputHandler.GetRunningInput();
            _CurrentWeapon.IsGrounded = _CharacterMotor.IsGrounded;
        }

        private void ApplyWeaponFire() => _CurrentWeapon.Fire();
        private void ApplyWeaponAim() => _CurrentWeapon.SetAim(_InputHandler.GetRightClickHeld());
    }
}
