
using UnityEngine;
using Types;
using CoreLib;

namespace Main
{
    public class Settings: MonoBehaviour
    {
        public static Settings Current;
        public int TruckCapacity;
        public float ClosingTime;
        public float RidingTime;
        public int MoneyForBox;

        void Awake()
        {
            Current = this;
        }
    }
}
