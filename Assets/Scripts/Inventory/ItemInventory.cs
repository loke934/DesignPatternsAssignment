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
        [SerializeField, Range(1,10)] 
        private int amountToAdd = 1;
        [SerializeField, Range(1,6)] 
        private int winAmount = 3;
        private int placedWasteBins;
        private int craftingCost = 3;
        
        public delegate void EventDelegate();
        public delegate void InventoryDelegate(ItemType itemType, int amount);
        public event InventoryDelegate OnUpdateInventory;
        public event EventDelegate OnCanCraft;
        public event EventDelegate OnCannotCraft;
        public event EventDelegate OnAllBinsPlaced;
        
        public int CraftingCost => craftingCost;
        
        public static ItemInventory Instance
        {
            get => instance;
            private set => instance = value;
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
            
            inventoryLookUp = new Dictionary<ItemType, int>
            {
                {ItemType.Can, 0},
                {ItemType.Plastic, 0},
                {ItemType.Glass, 0},
                {ItemType.WasteBin, 0}
            };
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
                UpdateCraftingStatus();
            }
            OnUpdateInventory?.Invoke(itemType, inventoryLookUp[itemType]);
        }

        public void RemoveFromInventory(ItemType itemType, int amount = 1)
        {
            if (GetAmountFromInventory(itemType) > 0)
            {
                inventoryLookUp[itemType] -= amount;
                if (itemType == ItemType.Can)
                {
                    UpdateCraftingStatus();
                }
                OnUpdateInventory?.Invoke(itemType, inventoryLookUp[itemType]);
            }
        }

        private void UpdateCraftingStatus()
        {
            if (inventoryLookUp[ItemType.Can] >= craftingCost)
            {
                OnCanCraft?.Invoke();
            }
            else if (inventoryLookUp[ItemType.Can] <= craftingCost)
            {
                OnCannotCraft?.Invoke();
            }
        }

        public void PlaceWasteBin(Vector3 position, Vector3 direction)
        {
            if (GetAmountFromInventory(ItemType.WasteBin) > 0)
            {
                RemoveFromInventory(ItemType.WasteBin);
                Pool.Instance.GetPoolObject(ItemType.WasteBin, position + direction);
                placedWasteBins++;
                if (placedWasteBins >= winAmount)
                {
                    OnAllBinsPlaced?.Invoke();
                }
            }
        }
    }
}

