using System;
using CoreLib;
using Types;
using UnityEngine;

namespace Main
{
    public class StageReducer : StageReducerT<Stage>
    {
        private void Start()
        {
            Debug.Log("Start");
            AddEnterAction(Stage.Init, () =>
            {
                Debug.Log("Init");
                Jobs.Add(() => EmitStageChange(Stage.Play));
            });
            AddEnterAction(Stage.Play, () =>
            {
            });
                
            EmitStageChange(Stage.Init);
        }
    }
}
