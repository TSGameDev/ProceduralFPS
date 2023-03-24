using UnityEngine;

namespace TSGameDev.FPS.WeaponSystem
{
    public interface IWeapon
    {
        public Vector2 MovementInput { set; }
        public Vector2 MouseInput { set; }
        public bool IsRunning { set; }
        public bool IsGrounded { set; }

        public void Fire();
        public void SetAim(bool _IsAiming);
        public void Reload();
    }
}
