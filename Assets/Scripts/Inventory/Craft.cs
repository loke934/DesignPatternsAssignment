using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Factory;

namespace Inventory
{
    public class Craft : MonoBehaviour
    {
        private int craftingAmount;
        private void Start()
        {
            craftingAmount = ItemInventory.Instance.CraftingAmount;
        }

        public void CraftWasteBin()
        {
            if (ItemInventory.Instance.GetAmountFromInventory(ItemType.Can) >= craftingAmount)
            {
                ItemInventory.Instance.RemoveFromInventory(ItemType.Can, craftingAmount);
                ItemInventory.Instance.AddToInventory(ItemType.Wastebin, 1);
            }
        }
    }
}

