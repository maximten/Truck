using UnityEngine;

namespace Behaviors
{
    public class BoxBehavior : MonoBehaviour
    {
        public bool InTruck = false;

        private Rigidbody _rigidbody;

        void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void SetAt(Transform newTransform)
        {
            transform.position = newTransform.position;
            transform.rotation = newTransform.rotation;
            _rigidbody.isKinematic = false;
            _rigidbody.useGravity = true;
            _rigidbody.Move(newTransform.position, newTransform.rotation);
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
