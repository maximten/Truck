using UnityEngine;
using Main;
using Types;
using CoreLib;

namespace Behaviors
{
    public class LookBehavior: MonoBehaviour
    {
        [SerializeField] private float HorizontalSens;
        [SerializeField] private float VerticalSens;
        [SerializeField] private float MaxVerticalAngle;
        [SerializeField] private float MinVerticalAngle;

        private Vector2 _currMousePos;
        private Vector2 _prevMousePos;

        private float _verticalAngle;
        private Camera _camera;

        void OnEnable()
        {
            _camera = GetComponentInChildren<Camera>();
            EventEmitterT<InputEvents>.Subscribe<Vector2>(InputEvents.Look, HandleMousePos);
        }

        void OnDisable()
        {
            EventEmitterT<InputEvents>.Unsubscribe<Vector2>(InputEvents.Look, HandleMousePos);
        }

        void Update()
        {
            HandleLook();
        }

        void HandleLook()
        {
            if (StageReducer.Current.Stage != Stage.Play)
                return;
            var diff = _currMousePos - _prevMousePos;
            var horizontalDiff = diff.x * HorizontalSens * Time.deltaTime;
            var verticalDiff = -diff.y * VerticalSens * Time.deltaTime;
            transform.localRotation *= Quaternion.Euler(new Vector3(0, horizontalDiff, 0));
            _verticalAngle = Mathf.Clamp(_verticalAngle + verticalDiff, MinVerticalAngle, MaxVerticalAngle);
            _camera.transform.localRotation = Quaternion.Euler(_verticalAngle, 0, 0);
            _prevMousePos = _currMousePos;
        }

        void HandleMousePos(Vector2 pos)
        {
            _currMousePos = pos;
        }
    }
}
