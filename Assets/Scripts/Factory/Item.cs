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
        WasteBin
    }
    public abstract class Item : MonoBehaviour
    {
        public bool IsActive => gameObject.activeInHierarchy;
    }
}

