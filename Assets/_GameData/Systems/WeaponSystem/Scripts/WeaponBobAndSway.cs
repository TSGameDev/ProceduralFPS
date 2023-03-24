using System.Collections;
using System.Collections.Generic;
using System.Timers;
using TSGameDev.FPS.Movement;
using UnityEngine;
using UnityEngine.UIElements;

namespace TSGameDev.FPS.WeaponSystem
{
    public class WeaponBobAndSway : MonoBehaviour
    {
        #region Settings

        [Header("Sway Settings")]
        [SerializeField] private float step = 0.01f;
        [SerializeField] private float maxStepDistance = 0.06f;
        private Vector3 _SwayPos;

        [Header("Sway Rotation")]
        [SerializeField] private float rotationStep = 4f;
        [SerializeField] private float maxRotationStep = 5f;
        private Vector3 _SwayEulerRot;

        [Header("Smoothers")]
        [SerializeField] private float posSmooth = 10f;
        [SerializeField] private float rotSmooth = 12f;

        [Header("Bobbing")]
        [SerializeField] private Vector3 _TravelLimit = Vector3.one * 0.025f;
        [SerializeField] private Vector3 _BobLimit = Vector3.one * 0.01f;
        [SerializeField] private float bobWalkingAmount;
        [SerializeField] private float bobRunningAmount;

        private float _SpeedCurve;
        private float _CurveSin { get => Mathf.Sin(_SpeedCurve); }
        private float _CurveCos { get => Mathf.Cos(_SpeedCurve); }
        private Vector3 _BobPos;

        [Header("Bob Rotation")]
        [SerializeField] Vector3 bobRotMultiplier;
        private Vector3 _BobEulerRot;

        #endregion

        #region Getter

        public Vector3 GetCurrentSwayBobPos() => _CurrentSwayBobPos;
        public Quaternion GetCurrentSwayBobRot() => _CurrentSwayBobRot;

        #endregion

        #region Private Variables

        private Weapon _Weapon;
        private Vector3 _CurrentSwayBobPos;
        private Quaternion _CurrentSwayBobRot;

        #endregion

        private void Awake()
        {
            _Weapon = GetComponent<Weapon>();
        }

        private void Update()
        {
            Sway();
            SwayRotation();

            _SpeedCurve += Time.deltaTime * (_Weapon.isGrounded ? _Weapon.movementInput.magnitude : 1f) + 0.01f;

            BobOffset();
            BobRotation();

            ApplySwayAndBob();
        }

        private void Sway()
        {
            Vector3 invertLook = _Weapon.mouseInput * -step;
            invertLook.x = Mathf.Clamp(invertLook.x, -maxStepDistance, maxStepDistance);
            invertLook.y = Mathf.Clamp(invertLook.y, -maxStepDistance, maxStepDistance);

            _SwayPos = invertLook;
        }

        private void SwayRotation()
        {
            Vector2 invertLook = _Weapon.mouseInput * -rotationStep;
            invertLook.x = Mathf.Clamp(invertLook.x, -maxRotationStep, maxRotationStep);
            invertLook.y = Mathf.Clamp(invertLook.y, -maxRotationStep, maxRotationStep);

            _SwayEulerRot = new(invertLook.y, invertLook.x, invertLook.x);
        }

        private void BobOffset()
        {
            _BobPos.x = (_CurveCos * _BobLimit.x * (_Weapon.isGrounded ? 1 : 0)) - 
                (_Weapon.movementInput.x * (_Weapon.isRunning ? bobRunningAmount : bobWalkingAmount) * _TravelLimit.x);

            _BobPos.y = (_CurveSin * _BobLimit.y) - (_Weapon.movementInput.y * (_Weapon.isRunning ? bobRunningAmount : bobWalkingAmount) * _TravelLimit.y);

            _BobPos.z = -(_Weapon.movementInput.y * (_Weapon.isRunning ? bobRunningAmount : bobWalkingAmount) * _TravelLimit.z);
        }

        private void BobRotation()
        {
            bool IsMoving = _Weapon.movementInput != Vector2.zero ? true : false;

            _BobEulerRot.x = IsMoving ? bobRotMultiplier.x * (Mathf.Sin(2 * _SpeedCurve)) : bobRotMultiplier.x * (Mathf.Sin(2 * _SpeedCurve) / 2);
            _BobEulerRot.y = IsMoving ? bobRotMultiplier.y * _CurveCos : 0;
            _BobEulerRot.z = IsMoving ? bobRotMultiplier.z * _CurveCos * _Weapon.movementInput.x : 0;
        }

        private void ApplySwayAndBob()
        {
            _CurrentSwayBobPos = Vector3.Lerp(transform.localPosition, _SwayPos + _BobPos, Time.deltaTime * posSmooth);
            _CurrentSwayBobRot = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(_SwayEulerRot) * Quaternion.Euler(_BobEulerRot), Time.deltaTime * rotSmooth);

            /*
            transform.localPosition = Vector3.Lerp(transform.localPosition, _SwayPos + _BobPos, Time.deltaTime * posSmooth);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(_SwayEulerRot) * Quaternion.Euler(_BobEulerRot), Time.deltaTime * rotSmooth);
            */
        }
    }
}
