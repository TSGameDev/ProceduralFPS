using System.Collections;
using System.Collections.Generic;
using System.Timers;
using TSGameDev.FPS.Movement;
using UnityEngine;

namespace TSGameDev.FPS.WeaponSystem
{
    public class WeaponBobAndSway : MonoBehaviour
    {
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

        private float _SpeedCurve;
        private float _CurveSin { get => Mathf.Sin(_SpeedCurve); }
        private float _CurveCos { get => Mathf.Cos(_SpeedCurve); }
        private Vector3 _BobPos;

        [Header("Bob Rotation")]
        [SerializeField] Vector3 bobRotMultiplier;
        private Vector3 _BobEulerRot;

        private InputHandler _RootInputHandler;
        private MotorHandlerCC _RootMotorHandler;
        private Vector2 _MouseInput;
        private Vector2 _MovementInput;
        private bool _IsRunning;

        private void Awake()
        {
            _RootInputHandler = transform.root.gameObject.GetComponent<InputHandler>();
            _RootMotorHandler = transform.root.gameObject.GetComponent<MotorHandlerCC>();
            _IsRunning = _RootInputHandler.GetRunningInput();
        }

        private void Update()
        {
            GetPlayerInputs();

            Sway();
            SwayRotation();

            _SpeedCurve += Time.deltaTime * (_RootMotorHandler.isGrounded ? _RootInputHandler.GetMovementInput().magnitude : 1f) + 0.01f;

            BobOffset();
            BobRotation();

            ApplySwayAndBob();
        }

        private void GetPlayerInputs()
        {
            _MouseInput = _RootInputHandler.GetMouseInput();
            _MovementInput = _RootInputHandler.GetMovementInput();
        }

        private void Sway()
        {
            Vector3 invertLook = _MouseInput * -step;
            invertLook.x = Mathf.Clamp(invertLook.x, -maxStepDistance, maxStepDistance);
            invertLook.y = Mathf.Clamp(invertLook.y, -maxStepDistance, maxStepDistance);

            _SwayPos = invertLook;
        }

        private void SwayRotation()
        {
            Vector2 invertLook = _MouseInput * -rotationStep;
            invertLook.x = Mathf.Clamp(invertLook.x, -maxRotationStep, maxRotationStep);
            invertLook.y = Mathf.Clamp(invertLook.y, -maxRotationStep, maxRotationStep);

            _SwayEulerRot = new(invertLook.y, invertLook.x, invertLook.x);
        }

        private void BobOffset()
        {
            _BobPos.x = (_CurveCos * _BobLimit.x * (_RootMotorHandler.isGrounded ? 1 : 0)) - 
                (_MovementInput.x * (_IsRunning ? _RootMotorHandler.GetRunningSpeed() : _RootMotorHandler.GetWalkingSpeed()) * _TravelLimit.x);

            _BobPos.y = (_CurveSin * _BobLimit.y) - (_MovementInput.y * (_IsRunning ? _RootMotorHandler.GetRunningSpeed() : _RootMotorHandler.GetWalkingSpeed()) * _TravelLimit.y);

            _BobPos.z = -(_MovementInput.y * (_IsRunning ? _RootMotorHandler.GetRunningSpeed() : _RootMotorHandler.GetWalkingSpeed()) * _TravelLimit.z);
        }

        private void BobRotation()
        {
            bool IsMoving = _MovementInput != Vector2.zero ? true : false;

            _BobEulerRot.x = IsMoving ? bobRotMultiplier.x * (Mathf.Sin(2 * _SpeedCurve)) : bobRotMultiplier.x * (Mathf.Sin(2 * _SpeedCurve) / 2);
            _BobEulerRot.y = IsMoving ? bobRotMultiplier.y * _CurveCos : 0;
            _BobEulerRot.z = IsMoving ? bobRotMultiplier.z * _CurveCos * _MovementInput.x : 0;
        }

        private void ApplySwayAndBob()
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, _SwayPos + _BobPos, Time.deltaTime * posSmooth);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(_SwayEulerRot) * Quaternion.Euler(_BobEulerRot), Time.deltaTime * rotSmooth);
        }
    }
}
