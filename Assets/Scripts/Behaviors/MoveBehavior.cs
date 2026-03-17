using UnityEngine;
using CoreLib;
using Types;
using Main;
using Data;

namespace Behaviors
{
    public class MoveBehavior: MonoBehaviour
    {
        [SerializeField] private float Speed = 3;

        private CharacterController _characterController;

        void OnEnable()
        {
            _characterController = GetComponent<CharacterController>();
            EventEmitterT<InputEvents>.Subscribe<Vector3>(InputEvents.Move, HandleMove);
        }

        void OnDisable()
        {
            EventEmitterT<InputEvents>.Unsubscribe<Vector3>(InputEvents.Move, HandleMove);
        }

        void HandleMove(Vector3 movement)
        {
            if (StageReducer.Current.Stage != Stage.Play)
                return;

            movement = transform.TransformDirection(movement) * Speed;
            movement.y = -Global.G;
            movement *= Time.fixedDeltaTime;
            _characterController.Move(movement);
        }
    }
}
