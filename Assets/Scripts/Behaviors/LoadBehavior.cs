using UnityEngine;
using Main;
using Types;
using CoreLib;

namespace Behaviors
{
    public class LoadBehavior: MonoBehaviour
    {
        private BoxHandlingBehavior _boxHandlingBehavior;
        private LoadZoneBehavior _loadZoneBehavior;

        public bool _inLoadZone = false;

        void Awake()
        {
            _boxHandlingBehavior = GetComponent<BoxHandlingBehavior>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag("LoadZone"))
                return;
            _inLoadZone = true;
            _loadZoneBehavior = other.gameObject.GetComponent<LoadZoneBehavior>();
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.gameObject.CompareTag("LoadZone"))
                return;
            _inLoadZone = false;
        }

        void OnEnable()
        {
            EventEmitterT<InputEvents>.Subscribe<bool>(InputEvents.Interact, HandleInteraction);
        }

        void OnDisable()
        {
            EventEmitterT<InputEvents>.Unsubscribe<bool>(InputEvents.Interact, HandleInteraction);
        }

        void HandleInteraction(bool hasInteraction)
        {
            if (!hasInteraction)
                return;
            if (StageReducer.Current.Stage != Stage.Play)
                return;
            if (!_inLoadZone)
                return;
            if (!_loadZoneBehavior.ParkZoneBehavior.HasTruck)
                return;
            var truck = _loadZoneBehavior.ParkZoneBehavior.TruckBehavior;
            if (_boxHandlingBehavior.HasBox && truck.HasCapacity())
            {
                truck.Put(_boxHandlingBehavior.Box);
                _boxHandlingBehavior.Box.InTruck = true;
                _boxHandlingBehavior.HasBox = false;
                return;
            }
            truck.Send();
            return;
        }
    }
}

