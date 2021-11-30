using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerInputComponent : MonoBehaviour
    {
        private Vector2 playerInput;
        private bool isWantingJump;

        public Vector2 PlayerInput => playerInput;
        public bool IsWantingJump
        {
            get => isWantingJump;
            set => isWantingJump = value;
        }

        public void ReadInput()
        {
            playerInput.x = Input.GetAxis("Horizontal");
            playerInput.y = Input.GetAxis("Vertical");
            playerInput = Vector2.ClampMagnitude(playerInput, 1f);
            isWantingJump |= Input.GetButtonDown("Jump");
        }
    }
}

