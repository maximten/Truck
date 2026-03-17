using UnityEngine;
using Main;
using Types;
using CoreLib;

namespace Behaviors
{
    public class LoadBehavior: MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag("LoadZone"))
                return;
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.gameObject.CompareTag("LoadZone"))
                return;
        }
    }
}

