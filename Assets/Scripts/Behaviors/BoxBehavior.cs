using UnityEngine;

namespace Behaviors
{
    public class BoxBehavior : MonoBehaviour
    {
        private Rigidbody _rigidbody;

        void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void Pickup()
        {
            _rigidbody.isKinematic = true;
            _rigidbody.useGravity = false;
        }

        void LateUpdate()
        {
            if (!_rigidbody.isKinematic)
                return;
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
        }
    }
}
