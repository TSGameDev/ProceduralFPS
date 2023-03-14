using UnityEngine;
using Sirenix.OdinInspector;

public class MotorHandler : MonoBehaviour
{
    [SerializeField] private bool useCharacterController = true;
    [SerializeField] private bool useRigidBody = false;
    [SerializeField] private Transform directionReference;
    [SerializeField] private float speed;

    [HideInInspector]
    public Vector2 moveDir { private get; set; }

    private bool _IsGrounded;
    private float _Gravity = -9.81f;
    private CharacterController _CharacterController;

    private void Awake()
    {
        _CharacterController = GetComponent<CharacterController>(); 
    }

    private void Update()
    {
        _IsGrounded = _CharacterController.isGrounded;

        if (useCharacterController)
            CharacterControllerMovement();
        else if(useRigidBody)
            RigidBodyMovement();
    }

    private void CharacterControllerMovement()
    {
        Vector3 _PlayerMove;

        if (directionReference != null)
            _PlayerMove = directionReference.forward * moveDir.y + directionReference.right * moveDir.x;
        else
            _PlayerMove = gameObject.transform.forward * moveDir.y + gameObject.transform.right * moveDir.x;

        if (!_IsGrounded)
            _PlayerMove.y = _Gravity;
        else
            _PlayerMove.y = 0;

        _CharacterController.Move(_PlayerMove * speed * Time.deltaTime);
    }

    private void RigidBodyMovement()
    {
        Debug.Log("#Motor Handler# Rigid Body Movement Not Implimented Yet");
    }
}
