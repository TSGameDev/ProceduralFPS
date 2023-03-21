using UnityEngine;

namespace TSGameDev.FPS.WeaponSystem
{
    public class Weapon : MonoBehaviour, IWeapon
    {
        [Header("General Data")]
        [SerializeField] WeaponData weaponData;

        [Header("Shooting Data")]
        [SerializeField] Transform weaponFirePoint;
        private int _CurrentMagAmount;
        private int _CurrentAmmoAmount;
        private float _ShotTimer;
        private bool _CanShoot;

        [Header("Aiming Data")]
        [SerializeField] private Transform cameraTransform;
        [SerializeField] private Transform weaponHolderTransform;
        [SerializeField] private Transform sightTransform;
        [SerializeField] private float sightOffset;
        [SerializeField] private float aimingInTime;
        private Vector3 weaponAimInPos;
        private Vector3 weaponAimInPosVelocity;


        private void Awake()
        {
            _CurrentAmmoAmount = weaponData.GetAmmoAmount();
            _CurrentMagAmount= weaponData.GetMagAmount();
            _ShotTimer = 0;
            _CanShoot = true;
        }

        private void Update()
        {
            CountDownShotTimer();
        }

        public void Fire()
        {
            if(Physics.Raycast(weaponFirePoint.position, weaponFirePoint.forward, out RaycastHit hit, weaponData.GetWeaponRange()) && _CanShoot)
            {
                Debug.DrawLine(weaponFirePoint.position, hit.point, Color.red, 60);
                _CanShoot = false;
                _ShotTimer = weaponData.GetShotDelay();
                Debug.Log("#Weapon# Weapon Has Shot");
            }
        }

        public void Reload()
        {
            throw new System.NotImplementedException();
        }

        public void SetAim(bool _IsAiming)
        {
            Vector3 _TargetPos;

            if (_IsAiming)
                _TargetPos = cameraTransform.position + (transform.position - sightTransform.position) + (cameraTransform.forward * sightOffset);
            else
                _TargetPos = weaponHolderTransform.position;

            weaponAimInPos = transform.position;
            weaponAimInPos = Vector3.SmoothDamp(weaponAimInPos, _TargetPos, ref weaponAimInPosVelocity, aimingInTime);
            transform.position = weaponAimInPos;
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
    }
}
