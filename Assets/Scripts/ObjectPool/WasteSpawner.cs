using System;
using System.Collections;
using System.Collections.Generic;
using Inventory;
using UnityEngine;

namespace ObjectPool
{
    public class WasteSpawner : MonoBehaviour
    {
        [SerializeField] 
        public List<WasteSpawnSettings> spawnSettings;
        private bool isAllWasteBins;

        private void Start()
        {
            ItemInventory.Instance.OnAllBinsPlaced += StopSpawning;
            InitialSpawning();
            StartCoroutine(Spawn());
        }

        private void InitialSpawning()
        {
            for (int i = 0; i < spawnSettings.Count; i++)
            {
                for (int j = 0; j < spawnSettings[i].startAmount; j++)
                {
                    Vector3 position = Pool.Instance.GetRandomPosition();
                    Pool.Instance.GetPoolObject(spawnSettings[i].itemType, position);
                }
            }
        }

        private void StopSpawning() 
        {
            StopCoroutine(Spawn());
        }

        private IEnumerator Spawn()
        {
            while (!isAllWasteBins)
            {
                yield return new WaitForSeconds(60f);
            
                for (int i = 0; i < spawnSettings.Count; i++)
                {
                    for (int j = 0; j < spawnSettings[i].spawningAmount; j++)
                    {
                        Vector3 position = Pool.Instance.GetRandomPosition();
                        Pool.Instance.GetPoolObject(spawnSettings[i].itemType, position);
                    }
                }
            }
        }
    }
}

