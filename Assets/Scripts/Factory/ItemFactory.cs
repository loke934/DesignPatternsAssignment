using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ObjectPool;

namespace Factory
{
    public class ItemFactory : MonoBehaviour
    {
        [SerializeField] 
        public List<ItemSettings> itemSettings;
        [SerializeField] 
        private Pool objectPool;

        public void CreatePoolItems()
        {
            for (int i = 0; i < itemSettings.Count; i++)
            {
                List<Item> objectList = new List<Item>();

                for (int m = 0; m < itemSettings[i].AmountOfItems; m++)
                {
                    Item obj = Instantiate(itemSettings[i].ItemPrefab, objectPool.transform);
                    if (itemSettings[i].IsInactive)
                    {
                        obj.gameObject.SetActive(false);
                    }
                    obj.transform.position = objectPool.GetRandomPosition();
                    objectList.Add(obj);
                }
                objectPool.poolDictionary.Add(itemSettings[i].ItemType, objectList);
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
                    break;
                }
            }
            return itemToReturn;
        }
    }
}

