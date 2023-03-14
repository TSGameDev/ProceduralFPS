using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float HorizontalSensitivity;

    private InputHandler _InputHandler;
    private MotorHandler _MotorHandler;

    private void Awake()
    {
        _InputHandler = GetComponent<InputHandler>();
        _MotorHandler = GetComponent<MotorHandler>();
    }

    private void Update()
    {
        ApplyCharacterMovement();
        RotationPlayer();
    }

    private void ApplyCharacterMovement() => _MotorHandler.moveDir = _InputHandler.GetMovementInput();
    private void RotationPlayer()
    {
        float _MouseX = _InputHandler.GetMouseInput().x;
        gameObject.transform.Rotate(Vector3.up * _MouseX * HorizontalSensitivity * Time.deltaTime);
    }
}
