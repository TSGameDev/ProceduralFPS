using UnityEngine;

namespace TSGameDev.FPS.WeaponSystem
{
    public class Weapon : MonoBehaviour, IWeapon
    {
        [SerializeField] WeaponData weaponData;
        [SerializeField] Transform weaponFirePoint;
        
        private int _CurrentMagAmount;
        private int _CurrentAmmoAmount;
        private float _ShotTimer;
        bool _CanShoot;

        private void Awake()
        {
            _CurrentAmmoAmount = weaponData.GetAmmoAmount();
            _CurrentMagAmount= weaponData.GetMagAmount();
            _ShotTimer = 0;
            _CanShoot = true;
        }

        private void Update()
        {
            if(_ShotTimer > 0f)
            {
                _ShotTimer -= 1f * Time.time;
                _CanShoot = false;
            }
            else if(_ShotTimer <= 0)
            {
                _CanShoot = true;
                _ShotTimer = 0f;
            }
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
    }
}
