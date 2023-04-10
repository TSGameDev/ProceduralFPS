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
        private Animator _PlayerAnimator;
        private AnimEvents _OnAnimEvents;

        private void Awake()
        {
            _CurrentWeapon = debugWeapon;
            _InputHandler = GetComponent<InputHandler>();
            _CharacterMotor = GetComponent<IMotor>();
            _PlayerAnimator = GetComponentInChildren<Animator>();
            _OnAnimEvents = GetComponentInChildren<AnimEvents>();
            _OnAnimEvents.SetOnReload(debugWeapon.PerformReloadFunc);
        }

        private void Update()
        {
            UpdateWeaponInputs();

            if (_CurrentWeapon.IsWeaponAutomatic)
            {
                if (_InputHandler.GetLeftClickHeld())
                    ApplyWeaponFire();
            }
            else
            {
                if (_InputHandler.GetLeftClick())
                    ApplyWeaponFire();
            }

            ApplyWeaponAim();
        }

        private void UpdateWeaponInputs()
        {
            _CurrentWeapon.MovementInput = _InputHandler.GetMovementInput();
            _CurrentWeapon.MouseInput = _InputHandler.GetMouseInput();
            _CurrentWeapon.IsRunning = _InputHandler.GetRunningInput();
            _CurrentWeapon.IsGrounded = _CharacterMotor.IsGrounded;
            debugWeapon.PlayerAnimator = _PlayerAnimator;
        }

        private void ApplyWeaponFire() => _CurrentWeapon.Fire();
        private void ApplyWeaponAim() => _CurrentWeapon.SetAim(_InputHandler.GetRightClickHeld());
    }
}
