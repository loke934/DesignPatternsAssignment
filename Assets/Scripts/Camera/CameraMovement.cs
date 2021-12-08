using System;
using UnityEngine;

namespace Camera
{
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField] 
        private Transform target;

        private void Update()
        {
            transform.position = new Vector3(target.position.x, transform.position.y, target.position.z);
        }
    }
    
    

}
