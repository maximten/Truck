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
            _rigidbody.useGravity = false;
            _rigidbody.angularVelocity = Vector3.zero;
            _rigidbody.linearVelocity = Vector3.zero;
        }

        public void HoldAt(Transform newTransform)
        {
            _rigidbody.Move(
                    Vector3.Lerp(transform.position, newTransform.position, 0.9f),
                    Quaternion.Lerp(transform.rotation, newTransform.rotation, 0.9f));
        }
    }
}
