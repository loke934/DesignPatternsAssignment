using System;
using System.Collections;
using System.Collections.Generic;
using ObjectPool;
using UnityEngine;

namespace Factory
{
    public class WasteBehaviour : Item
    {
        public ItemType itemType;
        private const string TagPlayer = "Player";

        protected override ItemType ItemType => itemType;
        
        public WasteBehaviour(ItemType type)
        {
            itemType = type;
        }
        protected override void OnCollisionEnter(Collision other)
        {
            // if (other.gameObject.CompareTag(TagPlayer))
            // {
            //     //Invoke event for add? Or how reach Inventory? Singleton?
            //     ReturnToObjectPool();
            // }
        }
    }
}

