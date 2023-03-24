using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TSGameDev.FPS.Movement
{
    public class RotationHandlerCC : MonoBehaviour
    {
        #region Serialized Variables

        [Tooltip("The gameobject the camera is a child of or locked to from cinemachine. Aka the gameobject that can control the rotation of the camera object.")]
        [SerializeField] Transform cameraRotationLock;
        [Tooltip("The speed of the horizontal rotation for looking around")]
        [SerializeField] float horizontalSensitivity = 300f;
        [Tooltip("The speed of the vertical rotation for looking up/down")]
        [SerializeField] float verticalSensitivity = 300f;
        [Tooltip("The minimumal possible rotation of the camera rotation lock object. stops the player looking behind them when looking down.")]
        [SerializeField] float minVerticalPitch = -89f;
        [Tooltip("The maximum possible rotation of the camera rotation lock object. stops the player looking behind them when looking up.")]
        [SerializeField] float maxVerticalPitch = 89f;

        #endregion

        #region Setters

        //Setter for the player controller or any other script to pass in the mouse input.
        [HideInInspector]
        public Vector2 mouseInput { private get; set; }

        #endregion

        #region Private Varaibles

        //Caches for the current X and Y incriments
        private float _MouseX;
        private float _MouseY;

        #endregion

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
