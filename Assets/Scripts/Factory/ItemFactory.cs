using System.Collections.Generic;
using UnityEngine;
using ObjectPool;

namespace Factory
{
    public class ItemFactory : MonoBehaviour
    {
        [SerializeField] 
        private List<ItemSettings> itemSettings;
        private int numberOfObjects;

        public int NumberOfObjects
        {
            get => numberOfObjects;
            set => numberOfObjects -= value;
        }

        public void CreatePoolItems()
        {
            for (int i = 0; i < itemSettings.Count; i++)
            {
                List<Item> objectList = new List<Item>();

                for (int m = 0; m < itemSettings[i].AmountOfItems; m++)
                {
                    Item obj = Instantiate(itemSettings[i].ItemPrefab, Pool.Instance.transform);
                    obj.ItemType = itemSettings[i].ItemType;
                    obj.gameObject.SetActive(false);
                    objectList.Add(obj);
                    numberOfObjects++;
                }
                Pool.Instance.poolDictionary.Add(itemSettings[i].ItemType, objectList);
            }
        }

        public Item CreateItem(ItemType itemType)
        {
            Item itemToReturn = null;
            for (int i = 0; i < itemSettings.Count; i++)
            {
                if (itemSettings[i].ItemType == itemType)
                {
                    itemToReturn = Instantiate(itemSettings[i].ItemPrefab);
                    itemToReturn.ItemType = itemSettings[i].ItemType;
                    numberOfObjects++;
                    break;
                }
            }
            return itemToReturn;
        }
    }
}

