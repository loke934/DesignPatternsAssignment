using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Factory;
using TMPro;

namespace Inventory
{
    public class UiInventory : MonoBehaviour
    {
        [SerializeField] 
        private TextMeshProUGUI canText;
        [SerializeField] 
        private TextMeshProUGUI plasticText;
        [SerializeField] 
        private TextMeshProUGUI glassText;
        [SerializeField] 
        private TextMeshProUGUI wasteBinText;

        private void Start()
        {
            ItemInventory.Instance.OnAddToInventory += UpdateInventoryText;
        }

        private void UpdateInventoryText(ItemType itemType, int amout)
        {
            TextMeshProUGUI text = null;
            switch (itemType)
            {
                case ItemType.Can:
                    text = canText;
                    break;
                case ItemType.Plastic:
                    text = plasticText;
                    break;
                case ItemType.Glass:
                    text = glassText;
                    break;
                case ItemType.Wastebin:
                    text = wasteBinText;
                    break;
            }
            text.text = amout.ToString();
        }
    }
    
}

