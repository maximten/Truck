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

        public void SetAt(Transform transform)
        {
            _rigidbody.Move(transform.position, transform.rotation);
        }

        public void Pickup(Transform newParent)
        {
            _rigidbody.isKinematic = true;
            _rigidbody.useGravity = false;
            transform.parent = newParent;
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
