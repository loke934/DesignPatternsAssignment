using System;
using System.Collections;
using System.Collections.Generic;
using ObjectPool;
using UnityEngine;

namespace Factory
{
    public class BinBehaviour : Item
    {
        public ItemType itemType;
        private const string TagWaste = "Waste";

        public BinBehaviour(ItemType type)
        {
            itemType = type;
        }

        public override void Throw()
        {
            throw new System.NotImplementedException();
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag(TagWaste))
            {
                Pool.Instance.Return(other.gameObject);
            }
        }
    }
}

