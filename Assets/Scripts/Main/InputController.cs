using UnityEngine;
using Types;
using CoreLib;

namespace Main
{
    public class InputController: MonoBehaviour
    {
        void Update()
        {
            HandleMove();
            HandleLook();
        }

        void HandleMove()
        {
            var movement = new Vector3();
            
            if (Input.GetKey(KeyCode.W))
            {
                movement.z += 1;
            }
            if (Input.GetKey(KeyCode.S))
            {
                movement.z += -1;
            }
            if (Input.GetKey(KeyCode.D))
            {
                movement.x += 1;
            }
            if (Input.GetKey(KeyCode.A))
            {
                movement.x += -1;
            }

            EventEmitterT<InputEvents>.Emit(InputEvents.Move, movement);
        }

        void HandleLook()
        {
            Vector2 mousePos = Input.mousePosition;
            EventEmitterT<InputEvents>.Emit(InputEvents.Look, mousePos);
        }
    }
}

