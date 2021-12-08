using UnityEngine;

namespace Factory
{
    public enum ItemType
    {
        Undefined,
        Can,
        Plastic,
        Glass,
        WasteBin
    }
    public abstract class Item : MonoBehaviour
    {
        protected ItemType itemType;

        public ItemType ItemType
        {
            get => itemType;
            set => itemType = value;
        }

        public bool IsActive => gameObject.activeInHierarchy;

        public virtual void AddForceToItem(Vector3 direction){}
    }
}

