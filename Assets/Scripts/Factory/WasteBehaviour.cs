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

        public WasteBehaviour(ItemType type)
        {
            itemType = type;
        }
    }
}

