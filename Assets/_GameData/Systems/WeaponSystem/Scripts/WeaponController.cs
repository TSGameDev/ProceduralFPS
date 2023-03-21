using UnityEngine;

namespace TSGameDev.FPS.WeaponSystem
{
    public class WeaponController : MonoBehaviour
    {
        [SerializeField] private Weapon debugWeapon;

        private IWeapon _CurrentWeapon;
        private InputHandler _InputHandler;

        private void Awake()
        {
            _CurrentWeapon = debugWeapon;
            _InputHandler = GetComponent<InputHandler>();
        }

        private void Update()
        {
            if(_InputHandler.GetLeftClick())
                ApplyWeaponFire();

            ApplyWeaponAim();
        }

        private void ApplyWeaponFire() => _CurrentWeapon.Fire();
        private void ApplyWeaponAim() => _CurrentWeapon.SetAim(_InputHandler.GetRightClickHeld());
    }
}
