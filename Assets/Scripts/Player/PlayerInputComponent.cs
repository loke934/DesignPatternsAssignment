using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Factory;
using Inventory;
using ObjectPool;
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

            if (Input.GetKeyDown(KeyCode.C))
            {
                Throw(KeyCode.C);
            }
            if (Input.GetKeyDown(KeyCode.P))
            {
                Throw(KeyCode.P);
            }
            if (Input.GetKeyDown(KeyCode.G))
            {
                Throw(KeyCode.G);
            }
        }
        private void Throw(KeyCode key) //Todo where to put this?? Another player component?
        {
            ItemType item = ItemType.Undefined;
            switch (key)
            {
                case KeyCode.C:
                    item = ItemType.Can;
                    break;
                case KeyCode.P:
                    item = ItemType.Plastic;
                    break;
                case KeyCode.G:
                    item = ItemType.Glass;
                    break;
                default:
                    throw new InvalidEnumArgumentException();
            }

            if (ItemInventory.Instance.GetAmountFromInventory(item) > 0)
            {
                Pool.Instance.GetPoolObject(item, new Vector3(transform.position.x, transform.position.y, transform.position.z + 1.5f)).Throw();
                ItemInventory.Instance.RemoveFromInventory(item);
            }
                
        }
    }
}

