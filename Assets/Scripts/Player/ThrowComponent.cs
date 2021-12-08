using ObjectPool;
using Inventory;
using Factory;
using UnityEngine;

namespace Player
{
    public class ThrowComponent : MonoBehaviour
    {
        public void Throw(ItemType itemType)
        {
            if (itemType != ItemType.Undefined)
            {
                if (ItemInventory.Instance.GetAmountFromInventory(itemType) > 0) //Todo put in statement over this?
                {
                    Vector3 position = transform.position + transform.forward * 2;
                    Pool.Instance.GetPoolObject(itemType, position).AddForceToItem(transform.forward);
                    ItemInventory.Instance.RemoveFromInventory(itemType);
                }
            }
        }
    }
}

