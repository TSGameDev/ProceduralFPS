using UnityEngine;
using Sirenix.OdinInspector;

public class MotorHandler : MonoBehaviour
{
    [SerializeField] private bool useCharacterController = true;
    [SerializeField] private Transform directionReference;
    [SerializeField] private float speed;
    [SerializeField] private float runningSpeed;
    [SerializeField] private float jumpHeight;

    [HideInInspector]
    public Vector2 moveDir { private get; set; }
    [HideInInspector]
    public bool hasJumped { private get; set; }
    [HideInInspector]
    public bool isRunning { private get; set; }


    private Vector3 _PlayerVerticalVelocity;
    private bool _IsGrounded;
    private float _Gravity = -9.81f;
    private float _FallMultiplier = 2.5f;
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

        Debug.Log($"#Motor Handler# isRunning: {isRunning}");
    }

    private void CharacterControllerMovement()
    {
        HandleHorizontalMovement();
        HandleVerticalMovement();
    }

    private void HandleHorizontalMovement()
    {
        Vector3 _PlayerMove;

        if (directionReference != null)
            _PlayerMove = directionReference.forward * moveDir.y + directionReference.right * moveDir.x;
        else
            _PlayerMove = gameObject.transform.forward * moveDir.y + gameObject.transform.right * moveDir.x;

        if(isRunning)
            _CharacterController.Move(_PlayerMove * runningSpeed * Time.deltaTime);
        else
            _CharacterController.Move(_PlayerMove * speed * Time.deltaTime);
    }

    private void HandleVerticalMovement()
    {
        if (_IsGrounded && _PlayerVerticalVelocity.y < 0)
            _PlayerVerticalVelocity.y = 0;

        if (hasJumped && _IsGrounded)
            _PlayerVerticalVelocity.y += jumpHeight;

        _PlayerVerticalVelocity.y += _Gravity * Time.deltaTime * _FallMultiplier;
        _CharacterController.Move(_PlayerVerticalVelocity * Time.deltaTime);
    }
}
