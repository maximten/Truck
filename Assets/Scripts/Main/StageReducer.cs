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
            AddEnterAction(Stage.Init, () =>
            {
                Jobs.Add(() => EmitStageChange(Stage.Play));
            });
            AddEnterAction(Stage.Play, () =>
            {
                Cursor.visible = false; 
            });
            AddExitAction(Stage.Play, () => {
                Cursor.visible = true;
            });
                
            EmitStageChange(Stage.Init);
        }
    }
}
