using System;

namespace Factory
{
    [Serializable] 
    public struct ItemSettings
    {
        public ItemType ItemType;
        public int AmountOfItems;
        public bool IsInactive;
        public Item ItemPrefab;
    }
}

