using UnityEngine;

namespace TSGameDev.FPS.WeaponSystem
{
    public class WeaponClipProvention : MonoBehaviour
    {
        [Tooltip("The ignore layer for the clip prevention to stop weapon ADS from causing weapon rotation")]
        [SerializeField] private LayerMask ignoreLayer;
        [Tooltip("The transform of the object raycast projector.")]
        [SerializeField] private Transform clipProjector;
        [Tooltip("The distance from the clipProjector to check for collisions.")]
        [SerializeField] private float checkDis;
        [Tooltip("The speed at which the weapon rotations. This value is over a second.")]
        [SerializeField] private float rotationRate;
        [Tooltip("The compelte rotation of the weapon when compeletly up against a wall.")]
        [SerializeField] private Vector3 newRotDirection;

        private Vector3 _ClipWeaponRotation;
        private RaycastHit _Hit;
        private float _LerpPercentage;
        private bool _IsClipPrevented;

        public Vector3 GetClipWeaponRotation() => _ClipWeaponRotation;
        public bool GetIsClipPrevented() => _IsClipPrevented;

        void Update()
        {
            CalculatePercentageDistance();
            ApplyClipPreventionRotation();
        }

        private void CalculatePercentageDistance()
        {
            if (Physics.Raycast(clipProjector.position, clipProjector.forward, out _Hit, checkDis, ~ignoreLayer))
            {
                float _DistancePercentage = 1 - _Hit.distance / checkDis;
                _LerpPercentage += rotationRate * Time.deltaTime;
                if (_LerpPercentage >= _DistancePercentage)
                    _LerpPercentage = _DistancePercentage;

                _IsClipPrevented= true;
            }
            else
            {
                _LerpPercentage -= rotationRate * Time.deltaTime;
                _IsClipPrevented = false;
            }

            _LerpPercentage = Mathf.Clamp01(_LerpPercentage);
        }

        private void ApplyClipPreventionRotation()
        {
            _ClipWeaponRotation = Vector3.Lerp(Vector3.zero, newRotDirection, _LerpPercentage);
        }
    }
}
