using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Factory;
using Random = UnityEngine.Random;

namespace ObjectPool
{
    public class Pool : MonoBehaviour
    {
        private static Pool instance; //Bc only want one pool instance and need a global access)?)
        [SerializeField] 
        private ItemFactory itemFactory;
        public Dictionary<ItemType, List<Item>> poolDictionary = new Dictionary<ItemType, List<Item>>(); //Todo Change to queue?

        //pool unaware of what the items are, create in factory and pool has the instances
        public static Pool Instance
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
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;
            }
            
            itemFactory.CreatePoolItems();
        }

        // private void CreatePools()
        // {
        //     for (int i = 0; i < itemSettings.Count; i++)
        //     {
        //         List<Item> objectList = new List<Item>();
        //
        //         for (int m = 0; m < itemSettings[i].AmountOfItems; m++)
        //         {
        //             Item obj = Instantiate(itemSettings[i].ItemPrefab, transform);//REFER TO FACTORY FOR CREATION?
        //             if (itemSettings[i].IsInactive)
        //             {
        //                 obj.gameObject.SetActive(false);
        //             }
        //             obj.transform.position = GetRandomPosition();
        //             objectList.Add(obj);
        //         }
        //         poolDictionary.Add(itemSettings[i].ItemType, objectList);
        //     }
        // }

        public Item GetPoolObject(ItemType itemType, Vector3 position)
        {
            Item poolObject = null;
            for (int i = 0; i < poolDictionary[itemType].Count; i++)
            {
                if (!poolDictionary[itemType][i].IsActive)
                {
                    poolObject = poolDictionary[itemType][i];
                    break;
                }
            }

            if (poolObject == null)
            {
                poolObject = itemFactory.CreateItem(itemType);
                poolObject.transform.SetParent(transform);
                poolDictionary[itemType].Add(poolObject);
            }
            
            poolObject.gameObject.SetActive(true);
            poolObject.transform.position = position;
            return poolObject;
        }

        public void Return(GameObject gameObject)
        {
            gameObject.SetActive(false);
        }
        
        public Vector3 GetRandomPosition() //Todo so spawn pos is right on the ground
        {
            float x = Random.Range(-20f, 20f);
            float z = Random.Range(-20f, 20f);
            return new Vector3(x, 0.5f, z);
        }
    }
}

