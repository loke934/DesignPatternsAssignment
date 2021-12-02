using System;
using System.Collections;
using System.Collections.Generic;
using Factory;
using ObjectPool;
using UnityEngine;

namespace Inventory
{
    public class ItemInventory : MonoBehaviour
    {
        private static ItemInventory instance;
        private Dictionary<ItemType, int> inventoryLookUp;
        private int craftingAmount = 3;

        public int CraftingAmount => craftingAmount;

        public event Action<ItemType, int> OnUpDateInventory;
        public event Action OnCanCraft;
        public event Action OnCantCraft;
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

        public int GetAmountFromInventory(ItemType itemType)
        {
            return inventoryLookUp[itemType];
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

            OnUpDateInventory?.Invoke(itemType, inventoryLookUp[itemType]);
            if (itemType == ItemType.Can)
            {
                CheckIfCraftingTime();
            }
        }

        public void RemoveFromInventory(ItemType itemType, int amount)
        {
            inventoryLookUp[itemType] -= amount;
            if (itemType == ItemType.Can)
            {
                CheckIfCraftingTime();
            }
            OnUpDateInventory?.Invoke(itemType, inventoryLookUp[itemType]);
        }

        private void CheckIfCraftingTime()
        {
            if (inventoryLookUp[ItemType.Can] >= craftingAmount)
            {
                OnCanCraft?.Invoke();
            }
            else if (inventoryLookUp[ItemType.Can] <= craftingAmount)
            {
                OnCantCraft?.Invoke();
            }
        }

        public bool IsWastebinIsAvailable()
        {
            if (GetAmountFromInventory(ItemType.Wastebin) > 0)
            {
                return true;
            }
            return false;
        }

        public void PlaceWastebin(Vector3 position) //Todo in another place?? Run on input
        {
            if (IsWastebinIsAvailable())
            {
                RemoveFromInventory(ItemType.Wastebin,1);
                Pool.Instance.GetPoolObject(ItemType.Wastebin, new Vector3(position.x, position.y, position.z + 1f));
            }
        }
    }
}

