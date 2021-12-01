using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Factory
{
    public enum ItemType
    {
        Can,
        Plastic,
        Glass,
        Wastebin
    }
    public abstract class Item : MonoBehaviour
    {
        protected abstract ItemType ItemType { get; }
        public bool IsActive => gameObject.activeInHierarchy;
        
        protected abstract void OnCollisionEnter(Collision other);

        protected void ReturnToObjectPool()
        {
            gameObject.SetActive(false);
        }
    }
}

