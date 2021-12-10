using System;
using System.Collections;
using System.Collections.Generic;
using Inventory;
using UnityEngine;

namespace ObjectPool
{
    public class WasteSpawner : MonoBehaviour
    {
        [SerializeField, Range(5f, 120f)] 
        private float secondsBetweenSpawn = 30f;
        [SerializeField, Range(1, 10)] 
        private int numOfSpawns = 5;
        [SerializeField] 
        public List<WasteSpawnSettings> spawnSettings;
        private bool isGameOver;

        public delegate void EventDelegate();

        public event EventDelegate OnGameOver;

        private void OnEnable()
        {
            ItemInventory.Instance.OnAllBinsPlaced += StopSpawning;
        }

        private void OnDisable()
        {
            ItemInventory.Instance.OnAllBinsPlaced -= StopSpawning;
        }

        private void Start()
        {
            InitialSpawning();
            StartCoroutine(ContinuousSpawn());
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
            isGameOver = true;
            StopCoroutine(ContinuousSpawn());
        }

        private IEnumerator ContinuousSpawn()
        {
            if (!isGameOver)
            {
                for (int i = 0; i < numOfSpawns; i++)
                {
                    yield return new WaitForSeconds(secondsBetweenSpawn);

                    if (!isGameOver)
                    {
                        for (int o = 0; o < spawnSettings.Count; o++)
                        {
                            for (int m = 0; m < spawnSettings[o].spawningAmount; m++)
                            {
                                Vector3 position = Pool.Instance.GetRandomPosition();
                                Pool.Instance.GetPoolObject(spawnSettings[o].itemType, position);
                            }
                        }
                    }
                }
            }
            OnGameOver?.Invoke();
        }
    }
}

