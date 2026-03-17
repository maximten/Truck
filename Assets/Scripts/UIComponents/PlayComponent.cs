using Types;
using CoreUI;
using CoreLib;
using UnityEngine.UIElements;
using UnityEngine;
using Main;
using Behaviors;

namespace UIComponents
{
    public class PlayComponent: UIComponentT<UIName>
    {
        PlayComponent()
        {
            UIName = UIName.Play;
        }

        void OnEnable()
        {
            MainState.Current.SubscribeInt(MainField.Money, HandleChange);
            MainState.Current.SubscribeInt(MainField.BoxesInTruck, HandleChange);
            Render();
        }

        void OnDisable()
        {
            MainState.Current.UnsubscribeInt(MainField.Money, HandleChange);
            MainState.Current.UnsubscribeInt(MainField.BoxesInTruck, HandleChange);
        }

        void HandleChange(int value)
        {
            Render();
        }

        void Render()
        {
            var boxesLabel = Find<Label>("Boxes");
            var moneyLabel = Find<Label>("Money");
            boxesLabel.text = $"{MainState.Current.GetInt(MainField.BoxesInTruck)}/{Settings.Current.TruckCapacity} boxes";
            moneyLabel.text = $"{MainState.Current.GetInt(MainField.Money)}$";
        }
    }
}
