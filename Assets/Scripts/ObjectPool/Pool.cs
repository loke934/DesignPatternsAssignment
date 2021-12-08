using System.Collections.Generic;
using UnityEngine;
using Factory;
using Random = UnityEngine.Random;

namespace ObjectPool
{
    public class Pool : MonoBehaviour
    {
        private static Pool instance;
        [SerializeField] 
        private ItemFactory itemFactory;
        [SerializeField, Range(100,300)] 
        private int poolCapacity = 100;
        public Dictionary<ItemType, List<Item>> poolDictionary = new Dictionary<ItemType, List<Item>>();
        
        public static Pool Instance
        {
            get => instance;
            private set => instance = value;
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
            
            poolObject.transform.position = position;
            poolObject.gameObject.SetActive(true);
            return poolObject;
        }

        public void Return(GameObject gameObject)
        {
            if (itemFactory.NumberOfObjects > poolCapacity)
            {
                itemFactory.NumberOfObjects -= 1;
                Destroy(gameObject);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
        
        public Vector3 GetRandomPosition()
        {
            float x = Random.Range(-20f, 20f);
            float z = Random.Range(-20f, 20f);
            return new Vector3(x, 5f, z);
        }
    }
}

