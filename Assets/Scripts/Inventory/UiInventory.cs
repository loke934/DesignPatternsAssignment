using System;
using UnityEngine;
using Factory;
using ObjectPool;
using TMPro;
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
        [SerializeField] 
        private GameObject gameOverCanvas;
        [SerializeField] 
        private WasteSpawner wasteSpawner;
        private const string CanText = "Can";
        private const string CraftText = "CRAFT";
        
        private void Start()
        {
            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            ItemInventory.Instance.OnUpdateInventory += UpdateInventoryText;
            ItemInventory.Instance.OnCanCraft += ActivateCraftText;
            ItemInventory.Instance.OnCannotCraft += DeactivateCraftText;
            wasteSpawner.OnGameOver += GameOver;
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
                case ItemType.WasteBin:
                    text = wasteBinText;
                    break;
            }
            text.text = amount.ToString();
        }

        private void ActivateCraftText()
        {
            canButton.image.color = Color.green;
            canButtonText.text = CraftText;
        }
        private void DeactivateCraftText()
        {
            canButton.image.color = Color.yellow;
            canButtonText.text = CanText;
        }

        private void GameOver()
        {
            gameOverCanvas.SetActive(true);
        }
    }
}

