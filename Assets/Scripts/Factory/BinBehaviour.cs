
using ObjectPool;
using UnityEngine;

namespace Factory
{
    public class BinBehaviour : Item
    {
        private const string TagWaste = "Waste";

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag(TagWaste))
            {
                Pool.Instance.Return(other.gameObject);
            }
        }
    }
}

