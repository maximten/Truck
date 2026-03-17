using System;
using CoreLib;
using Types;

namespace Main
{
    public class MainState : StateBehaviorT<MainField>
    {
        public static MainState Current;
        
        private void Awake()
        {
            Current = this;

            RegisterInt(MainField.Money, 0);
            RegisterInt(MainField.BoxesInTruck, 0);
        }
    }
}
