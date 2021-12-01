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
        private const string TagWaste = "Waste";
        private const string TagWasteBin = "WasteBin";

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
                ItemInventory.Instance.AddToInventory(wasteBehaviour.itemType, 1);
            }
        }

        private void CheckIfCanJump(Collision collision)
        {
            for (int i = 0; i < collision.contactCount; i++)
            {
                Vector3 normal = collision.GetContact(i).normal;
                physicsComponent.IsOnGround |= normal.y >= physicsComponent.JumpFlexibility;
            }
        }
    }

}
