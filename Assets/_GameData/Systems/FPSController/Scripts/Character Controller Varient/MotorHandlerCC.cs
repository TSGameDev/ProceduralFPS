using UnityEngine;

namespace TSGameDev.FPS.Movement
{
    [RequireComponent(typeof(CharacterController))]
    public class MotorHandlerCC : MonoBehaviour, IMotor
    {
        #region Serialized Variables

        [Tooltip("The object for movement to be relivant from, aka makes moving forward relivant to this objects Transform.forward. If blank, movement is relivant to the object this script is on.")]
        [SerializeField] private Transform directionReference;
        [Tooltip("The walking/normal speed of this entity.")]
        [SerializeField] private float speed;
        [Tooltip("The running speed of this entity")]
        [SerializeField] private float runningSpeed;
        [Tooltip("The jump height of this entity, might need to be higher than expected due to gravity and other factors.")]
        [SerializeField] private float jumpHeight;

        #endregion

        #region Setters

        //Setter for external sources to determine the movement direction for this entity.
        [HideInInspector]
        public Vector2 moveDir { private get; set; }

        //Setter for external sources to determine if this entity has jumpped.
        [HideInInspector]
        public bool hasJumped { private get; set; }

        //Setter for external sources to determine if this entity is running.
        [HideInInspector]
        public bool isRunning { private get; set; }

        #endregion

        #region Getters

        public bool IsGrounded => _IsGrounded;

        #endregion

        #region Private Variables

        private CharacterController _CharacterController;

        private Vector3 _PlayerVerticalVelocity;

        private float _Gravity = -9.81f;
        private float _FallMultiplier = 2.5f; //Gravity Multiplier to make jumping more "heavy".

        private bool _IsGrounded;

        #endregion

        private void Awake()
        {
            _CharacterController = GetComponent<CharacterController>();
        }

        private void Update()
        {
            _IsGrounded = _CharacterController.isGrounded;

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

            if (isRunning)
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
}
