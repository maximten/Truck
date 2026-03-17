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
        [SerializeField] private GameObject Lid;
        [SerializeField] private Vector3 LidOpened;
        [SerializeField] private Vector3 LidClosed;
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
            MainState.Current.SetInt(MainField.BoxesInTruck, Storage.Count);
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
            yield return Coroutines.TimerAction(t => {
                Lid.transform.localPosition = Vector3.Lerp(LidOpened, LidClosed, t);
            }, Settings.Current.ClosingTime, 0, () => {
                Lid.transform.localPosition = LidClosed;
            });
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
                Lid.transform.localPosition = LidOpened;
                CurrentState = State.Wait;
            });
        }

        void Unload()
        {
            var moneyDiff = Storage.Count * Settings.Current.MoneyForBox;
            foreach (var box in Storage)
            {
                box.transform.parent = null;
                BoxPool.Current.Release(box);
            }
            Storage.Clear();
            var currentMoney = MainState.Current.GetInt(MainField.Money);
            MainState.Current.SetInt(MainField.BoxesInTruck, Storage.Count);
            MainState.Current.SetInt(MainField.Money, currentMoney + moneyDiff);
        }
    }
}

