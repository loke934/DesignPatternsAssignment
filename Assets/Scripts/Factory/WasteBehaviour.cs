using UnityEngine;

namespace Factory
{
    public class WasteBehaviour : Item
    {
        [SerializeField, Range(300f, 700f)] 
        private float throwForceForward = 400f;
        [SerializeField, Range(0.5f, 2f)] 
        private float throwForceUp = 0.5f;
        private Rigidbody body;

        private void Awake()
        {
            body = GetComponent<Rigidbody>();
        }

        public override void Throw()
        {
            body.AddForce(new Vector3(0f, throwForceUp, throwForceForward));
        }
    }
}

