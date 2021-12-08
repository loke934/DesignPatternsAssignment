using UnityEngine;

namespace Factory
{
    public class WasteBehaviour : Item
    {
        [SerializeField, Range(300f, 700f)] 
        private float throwForceForward = 400f;
        private Rigidbody body;

        private void Awake()
        {
            body = GetComponent<Rigidbody>();
        }

        public override void AddForceToItem(Vector3 direction)
        {
            body.AddForce(direction * throwForceForward);
        }
    }
}

