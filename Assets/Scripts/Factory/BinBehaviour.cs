using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Factory
{
    public class BinBehaviour : Item
    {
        public ItemType itemType;
        private const string TagPlayer = "Player";

        protected override ItemType ItemType => itemType;
        
        public BinBehaviour(ItemType type)
        {
            itemType = type;
        }
        
        protected override void OnCollisionEnter(Collision other)
        {
            if (CompareTag(TagPlayer))
            {
                Debug.Log("Put in waste bin");
            }
        }
    }
}

