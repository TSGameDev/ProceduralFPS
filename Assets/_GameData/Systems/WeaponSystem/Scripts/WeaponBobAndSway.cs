using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TSGameDev.FPS.WeaponSystem
{
    public class WeaponBobAndSway : MonoBehaviour
    {
        [Header("Sway Settings")]
        [SerializeField] private float step = 0.01f;
        [SerializeField] private float maxStepDistance = 0.06f;
        private Vector3 swayPos;

        [Header("Sway Rotation")]
        [SerializeField] private float rotationStep = 4f;
        [SerializeField] private float maxRotationStep = 5f;
        Vector3 swayEulerRot;

        [Header("Smoothers")]
        [SerializeField] private float posSmooth = 10f;
        [SerializeField] private float rotSmooth = 12f;

        private InputHandler _InputHandler;
        private Vector2 _MouseInput;
        private Vector2 _MovementInput;

        private void Awake()
        {
            _InputHandler = transform.root.gameObject.GetComponent<InputHandler>();
        }

        private void Update()
        {
            GetPlayerInputs();

            Sway();
            SwayRotation();
            ApplySwayandSwayRotation();
        }

        private void GetPlayerInputs()
        {
            _MouseInput = _InputHandler.GetMouseInput();
            _MovementInput = _InputHandler.GetMovementInput();
        }

        private void Sway()
        {
            Vector3 invertLook = _MouseInput * -step;
            invertLook.x = Mathf.Clamp(invertLook.x, -maxStepDistance, maxStepDistance);
            invertLook.y = Mathf.Clamp(invertLook.y, -maxStepDistance, maxStepDistance);

            swayPos = invertLook;
        }

        private void SwayRotation()
        {
            Vector2 invertLook = _MouseInput * -rotationStep;
            invertLook.x = Mathf.Clamp(invertLook.x, -maxRotationStep, maxRotationStep);
            invertLook.y = Mathf.Clamp(invertLook.y, -maxRotationStep, maxRotationStep);

            swayEulerRot = new(invertLook.y, invertLook.x, invertLook.x);
        }

        private void ApplySwayandSwayRotation()
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, swayPos, Time.deltaTime * posSmooth);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(swayEulerRot), Time.deltaTime * rotSmooth);
        }
    }
}
