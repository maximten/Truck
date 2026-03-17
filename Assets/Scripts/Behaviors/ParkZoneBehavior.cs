using UnityEngine;
using Main;
using Types;
using CoreLib;

namespace Behaviors
{
    public class ParkZoneBehavior: MonoBehaviour
    {
        public bool HasTruck;
        public TruckBehavior TruckBehavior;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag("Truck"))
                return;
            HasTruck = true;
            TruckBehavior = other.gameObject.GetComponent<TruckBehavior>();
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.gameObject.CompareTag("Truck"))
                return;
            HasTruck = false;
        }
    }
}

