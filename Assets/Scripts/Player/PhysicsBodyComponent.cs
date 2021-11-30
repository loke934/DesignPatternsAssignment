using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PhysicsBodyComponent : MonoBehaviour
    {
        [SerializeField, Range(0f, 10f)] 
        private float maxSpeed = 6f;
        [SerializeField, Range(0f, 10f)] 
        private float maxAcceleration = 8f;
        [SerializeField, Range(0f, 100f)] 
        private float maxAirAcceleration = 4f;
        [SerializeField, Range(0f, 4f)] 
        private float jumpHeight = 2f;
        [SerializeField, Range(0f, 1f)] 
        private float jumpFlexibility = 1f; //Todo Change to better name
        
        private Vector3 velocity;
        private Vector3 desiredVelocity;
        private Rigidbody body;
        private bool isOnGround;

        private void Awake()
        {
            body = GetComponent<Rigidbody>();
        }

        public void SetDesiredVelocity(Vector2 playerInput)
        {
            desiredVelocity = new Vector3(playerInput.x, 0f, playerInput.y) * maxSpeed;
        }

        public void PlayerMovement(PlayerInputComponent inputComponent)
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

        private void OnCollisionEnter(Collision other)
        {
            EvaluateCollision(other);
        }

        private void EvaluateCollision(Collision collision)
        {
            for (int i = 0; i < collision.contactCount; i++)
            {
                Vector3 normal = collision.GetContact(i).normal;
                isOnGround |= normal.y >= jumpFlexibility;
            }
        }
    }
}

