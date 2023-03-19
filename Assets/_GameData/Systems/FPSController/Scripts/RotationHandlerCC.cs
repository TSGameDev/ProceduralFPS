using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TSGameDev.FPS.Movement
{
    public class RotationHandlerCC : MonoBehaviour
    {
        [SerializeField] float horizontalSensitivity;

        public Vector2 mouseInput { private get; set; }

        private void Update()
        {
            float _MouseX = mouseInput.x * horizontalSensitivity * Time.deltaTime;
            Vector3 _TargetRot = Vector3.up * _MouseX;
            transform.Rotate(_TargetRot);
        }
    }
}
