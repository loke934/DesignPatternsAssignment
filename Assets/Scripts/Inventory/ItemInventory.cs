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
        [SerializeField, Range(1,10)] 
        private int amountToAdd = 1;
        private int placedWasteBins = 0;
        
        public event Action<ItemType, int> OnUpDateInventory;
        public event Action OnCanCraft;
        public event Action OnCantCraft;
        public event Action OnAllBinsPlaced;
        
        public int CraftingAmount => craftingAmount;
        
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
            inventoryLookUp.Add(ItemType.Can,0);
            inventoryLookUp.Add(ItemType.Plastic,0);
            inventoryLookUp.Add(ItemType.Glass,0);
            inventoryLookUp.Add(ItemType.WasteBin,0);
        }

        public int GetAmountFromInventory(ItemType itemType)
        {
            return inventoryLookUp[itemType];
        }

        public void AddToInventory(ItemType itemType)
        {
            inventoryLookUp[itemType] += amountToAdd;
            if (itemType == ItemType.Can)
            {
                CheckIfCraftingTime();
            }
            OnUpDateInventory?.Invoke(itemType, inventoryLookUp[itemType]);
        }

        public void RemoveFromInventory(ItemType itemType, int amount = 1)
        {
            if (GetAmountFromInventory(itemType) > 0)
            {
                inventoryLookUp[itemType] -= amount;
                if (itemType == ItemType.Can)
                {
                    CheckIfCraftingTime();
                }
                OnUpDateInventory?.Invoke(itemType, inventoryLookUp[itemType]);
            }
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

        public void PlaceWasteBin(Vector3 position)
        {
            if (GetAmountFromInventory(ItemType.WasteBin) > 0)
            {
                RemoveFromInventory(ItemType.WasteBin);
                Pool.Instance.GetPoolObject(ItemType.WasteBin, new Vector3(position.x, position.y, position.z + 1f));
                placedWasteBins++;
                if (placedWasteBins >= 3)
                {
                    OnAllBinsPlaced?.Invoke();
                }
            }
        }
    }
}

