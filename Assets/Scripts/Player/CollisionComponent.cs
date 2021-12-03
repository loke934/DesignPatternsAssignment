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

        private void Awake()
        {
            physicsComponent = GetComponent<PhysicsBodyComponent>();
        }

        private void OnCollisionEnter(Collision other)
        {
            CheckIfCanJump(other);
            PickUpItem(other);
        }

        private void PickUpItem(Collision other)
        {
            if (other.gameObject.TryGetComponent(out WasteBehaviour wasteBehaviour))
            {
                Pool.Instance.Return(other.gameObject);
                ItemInventory.Instance.AddToInventory(wasteBehaviour.itemType);
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
