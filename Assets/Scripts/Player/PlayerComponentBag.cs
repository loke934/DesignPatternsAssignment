using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerComponentBag : MonoBehaviour
    {
        private PlayerInputComponent inputComponent;
        private PhysicsBodyComponent physicsComponent;

        private void Awake()
        {
            inputComponent = GetComponent<PlayerInputComponent>();
            physicsComponent = GetComponent<PhysicsBodyComponent>();
        }

        private void Update()
        {
            inputComponent.ReadInput();
            physicsComponent.SetDesiredVelocity(inputComponent.PlayerInput);
        }

        private void FixedUpdate()
        {
            physicsComponent.PlayerMovement(inputComponent);
        }
    }
}

