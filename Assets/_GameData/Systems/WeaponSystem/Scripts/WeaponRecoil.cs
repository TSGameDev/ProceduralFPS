using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TSGameDev.FPS.WeaponSystem
{
    public class WeaponRecoil : MonoBehaviour
    {
        [Header("Recoil Settings")]
        [Tooltip("The Amount of recoil that is applied to the X axis rotation")]
        [SerializeField] private float recoilX;
        [Tooltip("The Amount of recoil that is applied to the Y axis rotation")]
        [SerializeField] private float recoilY;
        [Tooltip("The Amount of recoil that is applied to the Z axis rotation")]
        [SerializeField] private float recoilZ;
        [Tooltip("The Amount of recoil that is applied to the Z axis position moving the gun back to simulate weapon kick.")]
        [SerializeField] private float kickBackZ;
        [Tooltip("Speed at which the weapon will go to the target position.")]
        [SerializeField] private float snappiness;
        [Tooltip("Speed at which the weapon will return to its inital position.")]
        [SerializeField] private float returnAmount;

        private Vector3 _InitialGunPos;
        private Vector3 _CurrentRot;
        private Vector3 _CurrentPos;
        private Vector3 _TargetRot;
        private Vector3 _TargetPos;

        public Vector3 GetCurrentRecoilRot() => _CurrentRot;
        public Vector3 GetCurrentRecoilPos() => _CurrentPos;

        private void Start()
        {
            _InitialGunPos = transform.localPosition;
        }

        private void Update()
        {
            ApplyRecoilRotations();
            ApplyRecoilKickback();
        }

        private void ApplyRecoilRotations()
        {
            _TargetRot = Vector3.Lerp(_TargetRot, Vector3.zero, Time.deltaTime * returnAmount);
            _CurrentRot = Vector3.Slerp(_CurrentRot, _TargetRot, Time.fixedDeltaTime * snappiness);
            //transform.localRotation = Quaternion.Euler(_CurrentRot);
        }

        private void ApplyRecoilKickback()
        {
            _TargetPos = Vector3.Lerp(_TargetPos, _InitialGunPos, Time.deltaTime * returnAmount);
            _CurrentPos = Vector3.Lerp(_CurrentPos, _TargetPos, Time.fixedDeltaTime * snappiness);
            //transform.localPosition= _CurrentPos;
        }

        public void ApplyRecoil()
        {
            _TargetPos -= new Vector3(0, 0, kickBackZ);
            _TargetRot += new Vector3(recoilX, Random.Range(-recoilY, recoilY), Random.Range(-recoilZ, recoilZ));
        }

    }
}
