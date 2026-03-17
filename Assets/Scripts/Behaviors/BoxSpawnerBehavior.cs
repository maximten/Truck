using UnityEngine;
using Main;
using Types;
using CoreLib;
using Pools;

namespace Behaviors
{
    public class BoxSpawnerBehavior: MonoBehaviour
    {
        [SerializeField] private float SpawnDelay;
        [SerializeField] private int BoxTargetCount;
        [SerializeField] private Transform SpawnOrigin;

        private int _boxCount;
        private float _currentSpawnDelay;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag("Box"))
                return;
            _boxCount++;
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.gameObject.CompareTag("Box"))
                return;
            _boxCount--;
        }

        void Update()
        {
            HandleDelay();
            HandleBoxSpawn();
        }

        void HandleDelay()
        {
            if (_currentSpawnDelay <= 0)
                return;
            _currentSpawnDelay -= Time.deltaTime;
        }

        void HandleBoxSpawn()
        {
            if (_boxCount >= BoxTargetCount)
                return;
            if (_currentSpawnDelay > 0)
                return;
            var box = BoxPool.Current.Get();
            box.SetAt(SpawnOrigin);
            box.gameObject.SetActive(true);
            _currentSpawnDelay = SpawnDelay;
        }
    }
}
