using UnityEngine;
using Factory;
using Inventory;

namespace Player
{
    public class Craft : MonoBehaviour
    {
        private int craftingAmount;
        private void Start()
        {
            craftingAmount = ItemInventory.Instance.CraftingCost;
        }

        public void CraftWasteBin()
        {
            if (ItemInventory.Instance.GetAmountFromInventory(ItemType.Can) >= craftingAmount)
            {
                ItemInventory.Instance.RemoveFromInventory(ItemType.Can, craftingAmount);
                ItemInventory.Instance.AddToInventory(ItemType.WasteBin);
            }
        }
    }
}

