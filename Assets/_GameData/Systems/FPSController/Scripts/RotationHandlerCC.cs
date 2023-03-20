using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TSGameDev.FPS.Movement
{
    public class RotationHandlerCC : MonoBehaviour
    {
        [SerializeField] Transform cameraRotationLock;
        [SerializeField] float horizontalSensitivity = 300f;
        [SerializeField] float verticalSensitivity = 300f;
        [SerializeField] float minVerticalPitch = -89f;
        [SerializeField] float maxVerticalPitch = 89f;

        public Vector2 mouseInput { private get; set; }

        private float _MouseX;
        private float _MouseY;

        private void Update()
        {
            ApplyHorizontalRotation();
            ApplyVerticalRotation();
        }

        private void ApplyHorizontalRotation()
        {
            _MouseX = mouseInput.x * horizontalSensitivity;
            Vector3 _TargetRot = Vector3.up * _MouseX;
            transform.Rotate(_TargetRot);
        }

        private void ApplyVerticalRotation() 
        {
            _MouseY += mouseInput.y * verticalSensitivity;
            _MouseY = ClampAngle(_MouseY, minVerticalPitch, maxVerticalPitch);
            cameraRotationLock.localRotation = Quaternion.Euler(_MouseY, 0f, 0f);
        }

        private float ClampAngle(float _LfAngle, float _Lfmin, float _LfMax)
        {
            if (_LfAngle < -360f)
                _LfAngle += 360f;
            if (_LfAngle > 360f)
                _LfAngle -= 360f;
            return Mathf.Clamp(_LfAngle, _Lfmin, _LfMax);
        }
    }
}
