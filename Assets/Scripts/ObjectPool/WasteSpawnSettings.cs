using System;
using Factory;

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

