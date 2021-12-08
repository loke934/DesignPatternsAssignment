
using UnityEngine;

namespace Player
{
    public class RotationComponent : MonoBehaviour
    {
        private PlayerInputComponent inputComponent;
        
        private void Awake()
        {
            inputComponent = GetComponent<PlayerInputComponent>();
        }
        
        private void Update()
        {
            SetRotation();
        }
        
        private void SetRotation()
        {
            Vector2 playerInput = inputComponent.PlayerInput;
            Quaternion rotation = transform.rotation;
            if (playerInput.x < 0)
            {
                rotation = Quaternion.AngleAxis(-90f, Vector3.up);
            }
            else if (playerInput.x > 0)
            {
                rotation = Quaternion.AngleAxis(90f, Vector3.up);
            }

            if (playerInput.y < 0)
            {
                rotation = Quaternion.AngleAxis(-180f, Vector3.up);
            }
            else if (playerInput.y > 0)
            {
                rotation = Quaternion.AngleAxis(0f, Vector3.up);
            }
            transform.rotation = rotation;
        }
    }
}

