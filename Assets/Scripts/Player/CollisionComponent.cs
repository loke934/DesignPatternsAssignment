using System;
using System.Collections;
using System.Collections.Generic;
using Factory;
using ObjectPool;
using Inventory;
using UnityEngine;

namespace Player
{
    public class CollisionComponent : MonoBehaviour
    {
        private PhysicsBodyComponent physicsComponent;
        private const string TagWasteBin = "WasteBin";
        private bool isAtWasteBin;

        public bool IsAtWasteBin => isAtWasteBin;

        private void Awake()
        {
            physicsComponent = GetComponent<PhysicsBodyComponent>();
        }

        private void OnCollisionEnter(Collision other)
        {
            CheckIfCanJump(other);
            
            if (other.gameObject.TryGetComponent(out WasteBehaviour wasteBehaviour))
            {
                Pool.Instance.Return(other.gameObject);
                ItemInventory.Instance.AddToInventory(wasteBehaviour.itemType);
            }
        }

        private void OnCollisionStay(Collision other)
        {
            if (other.gameObject.CompareTag(TagWasteBin))
            {
                isAtWasteBin = true;
            }
        }
        
        private void OnCollisionExit(Collision other)
        {
            if (other.gameObject.CompareTag(TagWasteBin))
            {
                isAtWasteBin = false;
            }
        }

        private void CheckIfCanJump(Collision collision)
        {
            for (int i = 0; i < collision.contactCount; i++)
            {
                Vector3 normal = collision.GetContact(i).normal;
                if (physicsComponent.IsOnGround || normal.y >= physicsComponent.JumpFlexibility)
                {
                    physicsComponent.IsOnGround = true;
                }
            }
        }
    }

}
