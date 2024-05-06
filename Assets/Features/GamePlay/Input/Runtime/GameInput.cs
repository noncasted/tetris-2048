using Common.DataTypes.Runtime.Reactive;
using Common.DataTypes.Runtime.Structs;
using Features.GamePlay.Input.Abstract;
using Global.Inputs.Constraints.Abstract;
using Global.Inputs.View.Abstract;
using Internal.Scopes.Abstract.Instances.Services;
using Internal.Scopes.Abstract.Lifetimes;
using Lean.Touch;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Features.GamePlay.Input.Runtime
{
    public class GameInput : IGameInput, IScopeLifetimeListener
    {
        public GameInput(Controls.GamePlayActions controls, IInputConstraintsStorage constraintsStorage)
        {
            _controls = controls;
            _constraintsStorage = constraintsStorage;
        }

        private readonly Controls.GamePlayActions _controls;
        private readonly IInputConstraintsStorage _constraintsStorage;

        private readonly ViewableDelegate<CoordinateDirection> _swipe = new();

        public IViewableDelegate<CoordinateDirection> Swipe => _swipe;

        public void OnLifetimeCreated(ILifetime lifetime)
        {
            _controls.Swipe.ListenPerformed(lifetime, OnKeyboardSwipe);
            LeanTouch.OnFingerSwipe += OnSwipe;
            lifetime.ListenTerminate(() => LeanTouch.OnFingerSwipe -= OnSwipe);
        }

        private void OnSwipe(LeanFinger finger)
        {
            if (_constraintsStorage[InputConstraints.Game] == false)
                return;

            var direction = finger.StartScreenPosition - finger.ScreenPosition;
            var angle = direction.ToAngle();

            switch (angle)
            {
                case > 45 and < 135:
                    _swipe.Invoke(CoordinateDirection.Up);
                    break;
                case >= 135 and <= 225f:
                    _swipe.Invoke(CoordinateDirection.Right);
                    break;
                case > 225 and < 315f:
                    _swipe.Invoke(CoordinateDirection.Down);
                    break;
                default:
                    _swipe.Invoke(CoordinateDirection.Left);
                    break;
            }
        }

        private void OnKeyboardSwipe(InputAction.CallbackContext context)
        {
            if (_constraintsStorage[InputConstraints.Game] == false)
                return;

            var direction = context.ReadValue<Vector2>();
            var angle = direction.ToAngle();

            switch (angle)
            {
                case > 45 and < 135:
                    _swipe.Invoke(CoordinateDirection.Down);
                    break;
                case >= 135 and <= 225f:
                    _swipe.Invoke(CoordinateDirection.Left);
                    break;
                case > 225 and < 315f:
                    _swipe.Invoke(CoordinateDirection.Up);
                    break;
                default:
                    _swipe.Invoke(CoordinateDirection.Right);
                    break;
            }
        }
    }
}