using UnityEngine;

namespace Camera
{
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField, Range(2, 10)] 
        private int offsetY = 5;
        [SerializeField, Range(7, 20)] 
        private int offsetZ = 10;
        [SerializeField] 
        private Transform target;

        private void Update()
        {
            transform.position = new Vector3(target.position.x, target.position.y + offsetY, target.position.z -offsetZ);
        }
    }
}
