using UnityEngine;

namespace Player
{
    public class PhysicsBodyComponent : MonoBehaviour
    {
        [SerializeField, Range(0f, 10f)] 
        private float maxSpeed = 6f;
        [SerializeField, Range(0f, 10f)] 
        private float maxAcceleration = 10f;
        [SerializeField, Range(0f, 100f)] 
        private float maxAirAcceleration = 5f;
        [SerializeField, Range(0f, 4f)] 
        private float jumpHeight = 2f;
        [SerializeField, Range(0f, 1f)] 
        private float jumpFlexibility = 1f;

        private Vector3 velocity;
        private Vector3 desiredVelocity;
        private Rigidbody body;
        private bool isOnGround;
        private PlayerInputComponent inputComponent;

        public float JumpFlexibility => jumpFlexibility;
        
        public bool IsOnGround
        {
            get => isOnGround;
            set => isOnGround = value;
        }

        private void Awake()
        {
            body = GetComponent<Rigidbody>();
            inputComponent = GetComponent<PlayerInputComponent>();
        }

        private void Update()
        {
            SetDesiredVelocity(inputComponent.PlayerInput);
        }

        private void FixedUpdate()
        {
            PlayerMovement();
        }

        private void SetDesiredVelocity(Vector2 playerInput)
        {
            desiredVelocity = new Vector3(playerInput.x, 0f, playerInput.y) * maxSpeed;
        }

        private void PlayerMovement()
        {
            velocity = body.velocity;
            float acceleration = isOnGround ? maxAcceleration : maxAirAcceleration;
            float maxSpeedChange = acceleration * Time.deltaTime;
            velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);
            velocity.z = Mathf.MoveTowards(velocity.z, desiredVelocity.z, maxSpeedChange);
            if (inputComponent.IsWantingJump)
            {
                inputComponent.IsWantingJump = false;
                if (isOnGround)
                {
                    Jump();
                }
            }
            body.velocity = velocity;
        }

        private void Jump()
        {
            float gravity = -2f * Physics.gravity.y;
            velocity.y += Mathf.Sqrt( gravity * jumpHeight);
            isOnGround = false;
        }
    }
}

