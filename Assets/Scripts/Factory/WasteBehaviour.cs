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
        private Rigidbody body;

        public WasteBehaviour(ItemType type)
        {
            itemType = type;
        }

        private void Awake()
        {
            body = GetComponent<Rigidbody>();
        }

        public override void Throw()
        {
            body.AddForce(new Vector3(0f, 0.5f, 300f));
        }
    }
}

