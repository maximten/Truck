using UnityEngine;
using Main;
using Types;
using CoreLib;
using System.Collections;
using System.Collections.Generic;
using Pools;

namespace Behaviors
{
    public class TruckBehavior: MonoBehaviour
    {
        [SerializeField] private Transform StorageOrigin;
        [SerializeField] private Transform ParkingSpot;
        [SerializeField] private Transform UnloadSpot;

        public enum State
        {
            Wait,
            Closing,
            Riding,
        }
        public State CurrentState;
        public List<BoxBehavior> Storage = new ();

        public bool HasCapacity()
        {
            return Storage.Count < Settings.Current.TruckCapacity;
        }

        public void Put(BoxBehavior box)
        {
            if (CurrentState != State.Wait)
                return;
            if (Storage.Count >= Settings.Current.TruckCapacity)
                return;
            Storage.Add(box);
            box.Pickup(StorageOrigin);
        }

        public void Send()
        {
            if (CurrentState != State.Wait)
                return;
            StartCoroutine(DoClosing());
        }

        private IEnumerator DoClosing()
        {
            CurrentState = State.Closing;
            yield return new WaitForSeconds(Settings.Current.ClosingTime);
            StartCoroutine(DoRiding());
        }

        private IEnumerator DoRiding()
        {
            CurrentState = State.Riding;
            var halfRidingTime = Settings.Current.RidingTime / 2;
            yield return Coroutines.TimerAction(t => {
                transform.position = Vector3.Lerp(ParkingSpot.transform.position, UnloadSpot.transform.position, t);
            }, halfRidingTime, 0, () => {
                transform.position = UnloadSpot.transform.position;
                Unload();
            });
            yield return Coroutines.TimerAction(t => {
                transform.position = Vector3.Lerp(UnloadSpot.transform.position, ParkingSpot.transform.position, t);
            }, halfRidingTime, 0, () => {
                transform.position = ParkingSpot.transform.position;
                CurrentState = State.Wait;
            });
        }

        void Unload()
        {
            foreach (var box in Storage)
            {
                box.transform.parent = null;
                BoxPool.Current.Release(box);
            }
            Storage.Clear();
        }
    }
}

