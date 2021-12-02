using System;
using System.Collections;
using System.Collections.Generic;
using Factory;
using Inventory;
using UnityEngine;

namespace Player
{
    public class PlayerInputComponent : MonoBehaviour
    {
        private Vector2 playerInput;
        private bool isWantingJump;
        private CollisionComponent collisionComponent;

        public Vector2 PlayerInput => playerInput;
        public bool IsWantingJump
        {
            get => isWantingJump;
            set => isWantingJump = value;
        }

        private void Awake()
        {
            collisionComponent = GetComponent<CollisionComponent>();
        }

        private void Update()
        {
            ReadInput();
        }

        private void ReadInput()
        {
            playerInput.x = Input.GetAxis("Horizontal");
            playerInput.y = Input.GetAxis("Vertical");
            playerInput = Vector2.ClampMagnitude(playerInput, 1f);
            isWantingJump |= Input.GetButtonDown("Jump");

            if (Input.GetKeyDown(KeyCode.F))
            {
                ItemInventory.Instance.PlaceWasteBin(transform.position);
            }

            if (collisionComponent.IsAtWasteBin)
            {
                if (Input.GetKeyDown(KeyCode.C))
                {
                    ItemInventory.Instance.RemoveFromInventory(ItemType.Can);
                }
                if (Input.GetKeyDown(KeyCode.P))
                {
                    ItemInventory.Instance.RemoveFromInventory(ItemType.Plastic);
                }
                if (Input.GetKeyDown(KeyCode.G))
                {
                    ItemInventory.Instance.RemoveFromInventory(ItemType.Glass);
                }
            }
        }
    }
}

