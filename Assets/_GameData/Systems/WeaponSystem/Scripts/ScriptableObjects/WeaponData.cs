using UnityEngine;

namespace TSGameDev.FPS.WeaponSystem
{
    [CreateAssetMenu(fileName = "New Weapon Data", menuName = "TSGameDev/Weapon System/New Weapon Data", order = 0)]
    public class WeaponData : ScriptableObject
    {
        [SerializeField] string _WeaponName;
        [SerializeField] int _MagAmount;
        [SerializeField] int _AmmoAmount;
        [SerializeField] float _WeaponRange;
        [SerializeField] float _TimeBetweenShots;
        [SerializeField] float _ShotDamage;

        public string GetWeaponName() => _WeaponName;
        public int GetMagAmount() => _MagAmount;
        public int GetAmmoAmount() => _AmmoAmount;
        public float GetWeaponRange() => _WeaponRange;
        public float GetShotDelay() => _TimeBetweenShots;
        public float GetShotDamage() => _ShotDamage;

    }
}
