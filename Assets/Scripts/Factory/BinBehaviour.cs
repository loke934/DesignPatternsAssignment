using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Factory
{
    public class BinBehaviour : Item
    {
        public ItemType itemType;

        public BinBehaviour(ItemType type)
        {
            itemType = type;
        }
    }
}

