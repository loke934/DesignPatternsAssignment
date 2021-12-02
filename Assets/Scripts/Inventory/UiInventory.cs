using System;
using System.Collections;
using UnityEngine;
using Factory;
using TMPro;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;

namespace Inventory
{
    public class UiInventory : MonoBehaviour
    {
        [SerializeField] 
        private TextMeshProUGUI canText;
        [SerializeField] 
        private Button canButton;
        [SerializeField] 
        private TextMeshProUGUI canButtonText;
        [SerializeField] 
        private TextMeshProUGUI plasticText;
        [SerializeField] 
        private TextMeshProUGUI glassText;
        [SerializeField] 
        private TextMeshProUGUI wasteBinText;

        private void Start()
        {
            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            ItemInventory.Instance.OnUpDateInventory += UpdateInventoryText;
            ItemInventory.Instance.OnCanCraft += ActivateCraftText;
            ItemInventory.Instance.OnCantCraft += DeactivateCraftText;
        }

        private void UpdateInventoryText(ItemType itemType, int amount)
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
            text.text = amount.ToString();
        }

        private void ActivateCraftText()
        {
            canButtonText.color = Color.green;
        }
        private void DeactivateCraftText()
        {
            canButtonText.color = Color.black;
        }
    }
}

