using System;
using System.Collections;
using System.Collections.Generic;
using Factory;
using UnityEngine;

namespace ObjectPool
{
    [Serializable]
    public struct WasteSpawnSettings
    {
        public ItemType itemType;
        public int startAmount;
        public int spawningAmount;
    }
    
}

