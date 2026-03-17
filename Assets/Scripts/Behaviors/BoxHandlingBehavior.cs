using UnityEngine;
using Main;
using Types;
using CoreLib;

namespace Behaviors
{
    public class BoxHandlingBehavior : MonoBehaviour
    {
        [SerializeField] private float RayLenght;
        [SerializeField] private Transform RayOrigin;
        [SerializeField] private Transform HoldOrigin;

        private bool _hasInteraction = false;
        public bool HasBox = false;
        public BoxBehavior Box;

        void OnEnable()
        {
            EventEmitterT<InputEvents>.Subscribe<bool>(InputEvents.Interact, HandleInteraction);
        }

        void OnDisable()
        {

            EventEmitterT<InputEvents>.Unsubscribe<bool>(InputEvents.Interact, HandleInteraction);
        }

        void FixedUpdate()
        {
            HandleRay();
        }

        void HandleInteraction(bool value)
        {
            _hasInteraction = value;
        }

        void HandleRay()
        {
            if (StageReducer.Current.Stage != Stage.Play)
                return;
            if (HasBox)
                return;
            if (!Physics.Raycast(RayOrigin.position, RayOrigin.forward, out var raycast, RayLenght))
                return;
            if (!raycast.collider.gameObject.CompareTag("Box"))
                return;
            if (!_hasInteraction)
                return;
            Box = raycast.collider.gameObject.GetComponent<BoxBehavior>();
            if (Box.InTruck)
                return;
            HasBox = true;
            Box.Pickup(HoldOrigin);
        }
    }
}
