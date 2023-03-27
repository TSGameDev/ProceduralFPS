using Cinemachine;
using UnityEngine;

namespace TSGameDev.FPS.WeaponSystem
{
    public class Weapon : MonoBehaviour, IWeapon
    {
        #region Variables

        [Header("General Data")]
        [SerializeField] WeaponData weaponData;

        private WeaponBobAndSway _WeaponBobAndSway;
        private WeaponClipProvention _WeaponClipProvention;

        private bool _IsClipPrevented;

        [Header("Shooting Data")]
        [SerializeField] Transform weaponFirePoint;

        private WeaponRecoil _WeaponRecoil;
        private int _CurrentMagAmount;
        private int _CurrentAmmoAmount;
        private float _ShotTimer;
        private bool _CanShoot;

        [Header("Aiming Data")]
        [SerializeField] private Transform weaponHolderTransform;
        [SerializeField] private Transform aimInTransform;
        [SerializeField] private Transform weaponReturnTransform;
        [SerializeField] private Transform sightTransform;
        [SerializeField] private float sightOffset;
        [SerializeField] private float aimingInTime;

        private Vector3 weaponAimInPos;
        private Vector3 weaponAimInPosVelocity;

        #endregion

        #region Setters

        public Vector2 movementInput { get; private set; }
        public Vector2 mouseInput { get; private set; }
        public bool isRunning { get; private set; }
        public bool isGrounded { get; private set; }

        public Vector2 MovementInput { set => movementInput = value; }
        public Vector2 MouseInput { set => mouseInput = value; }
        public bool IsRunning { set => isRunning = value; }
        public bool IsGrounded { set => isGrounded = value; }

        #endregion

        private void Awake()
        {
            _WeaponRecoil = GetComponent<WeaponRecoil>();
            _WeaponBobAndSway = GetComponent<WeaponBobAndSway>();
            _WeaponClipProvention = GetComponent<WeaponClipProvention>();
            _CurrentAmmoAmount = weaponData.GetAmmoAmount();
            _CurrentMagAmount= weaponData.GetMagAmount();
            _ShotTimer = 0;
            _CanShoot = true;
        }

        private void Update()
        {
            CountDownShotTimer();
            ApplyWeaponSwayandRecoil();

            _IsClipPrevented = _WeaponClipProvention.GetIsClipPrevented();
        }

        private void CountDownShotTimer()
        {
            if (_ShotTimer > 0f)
            {
                _ShotTimer -= 1f * Time.time;
                _CanShoot = false;
            }
            else if (_ShotTimer <= 0)
            {
                _CanShoot = true;
                _ShotTimer = 0f;
            }
        }

        private void ApplyWeaponSwayandRecoil()
        {
            transform.localPosition = _WeaponBobAndSway.GetCurrentSwayBobPos() + _WeaponRecoil.GetCurrentRecoilPos();

            if (_IsClipPrevented)
                transform.localRotation = Quaternion.Euler(_WeaponClipProvention.GetClipWeaponRotation());
            else
                transform.localRotation = _WeaponBobAndSway.GetCurrentSwayBobRot() * Quaternion.Euler(_WeaponRecoil.GetCurrentRecoilRot());
        }

        #region IWeapon Functions

        public void Fire()
        {
            if(_CanShoot)
            {
                _CanShoot = false;
                _ShotTimer = weaponData.GetShotDelay();
                _WeaponRecoil.ApplyRecoil();

                if (Physics.Raycast(weaponFirePoint.position, weaponFirePoint.forward, out RaycastHit hit, weaponData.GetWeaponRange()))
                {
                    Debug.DrawLine(weaponFirePoint.position, hit.point, Color.red, 60);
                }
            }
        }

        public void Reload()
        {
            throw new System.NotImplementedException();
        }

        public void SetAim(bool _IsAiming)
        {
            Vector3 _TargetPos;

            if (_IsAiming & !_IsClipPrevented)
                _TargetPos = aimInTransform.position + (transform.position - sightTransform.position) + (aimInTransform.forward * sightOffset);
            else
                _TargetPos = weaponReturnTransform.position;

            weaponAimInPos = weaponHolderTransform.position;
            weaponAimInPos = Vector3.SmoothDamp(weaponAimInPos, _TargetPos, ref weaponAimInPosVelocity, aimingInTime);
            weaponHolderTransform.position = weaponAimInPos;
        }

        #endregion

    }
}
