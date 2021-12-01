using System;
using System.Collections;
using System.Collections.Generic;
using Factory;
using UnityEngine;

namespace Inventory
{
    public class ItemInventory : MonoBehaviour
    {
        private static ItemInventory instance;
        private Dictionary<ItemType, int> inventoryLookUp;
        private int amoutOfCansForWasteBin = 3;
        private bool isCanCraft;

        public event Action<ItemType, int> OnAddToInventory;
        public static ItemInventory Instance
        {
            get
            {
                return instance;
            }
            private set
            {
                instance = value;
            }
        }

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;
            }
            
            inventoryLookUp = new Dictionary<ItemType, int>();
        }

        public void AddToInventory(ItemType itemType, int amount)
        {
            if (!inventoryLookUp.ContainsKey(itemType))
            {
                inventoryLookUp.Add(itemType,amount);
            }
            else
            {
                inventoryLookUp[itemType] += amount;
            }
            Debug.Log("Inventory" + itemType + "" + "amount: " +inventoryLookUp[itemType]);
            OnAddToInventory?.Invoke(itemType, inventoryLookUp[itemType]);
        }

        public void RemoveFromInventory(ItemType itemType, int amount)
        {
            inventoryLookUp[itemType] += amount;
            //invoke event
        }

        private void CheckIfCraftingTime()
        {
            if (inventoryLookUp[ItemType.Can] >= amoutOfCansForWasteBin)
            {
                isCanCraft = true;
                //invoke event that enables button for crafting
            }
            else if (inventoryLookUp[ItemType.Can] <= GetHashCode())
            {
                isCanCraft = false;
            }
        }
    }
}

