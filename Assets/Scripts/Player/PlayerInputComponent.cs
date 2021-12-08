using Factory;
using Inventory;
using UnityEngine;

namespace Player
{
    public class PlayerInputComponent : MonoBehaviour
    {
        private Vector2 playerInput;
        private bool isWantingJump;
        private ThrowComponent throwComponent;

        public Vector2 PlayerInput => playerInput;
        
        public bool IsWantingJump
        {
            get => isWantingJump;
            set => isWantingJump = value;
        }

        private void Awake()
        {
            throwComponent = GetComponent<ThrowComponent>();
        }

        private void Update()
        {
            ReadInput();
        }

        private void ReadInput()
        {
            playerInput.x = Input.GetAxis("Horizontal");
            playerInput.y = Input.GetAxis("Vertical");
            playerInput = Vector2.ClampMagnitude(playerInput, 1f);
            isWantingJump |= Input.GetButtonDown("Jump");
            
            if (Input.GetKeyDown(KeyCode.B))
            {
                ItemInventory.Instance.PlaceWasteBin(transform.position, transform.forward);
            }
            
            if (Input.GetKeyDown(KeyCode.C))
            {
                throwComponent.Throw(ItemType.Can);
            }
            if (Input.GetKeyDown(KeyCode.P))
            {
                throwComponent.Throw(ItemType.Plastic);
            }
            if (Input.GetKeyDown(KeyCode.G))
            {
                throwComponent.Throw(ItemType.Glass);
            }
        }
    }
}

