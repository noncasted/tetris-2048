using Global.Inputs.View.Abstract;
using Global.System.Updaters.Abstract;
using Internal.Scopes.Abstract.Instances.Services;
using Internal.Scopes.Abstract.Lifetimes;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Tools.AssembliesViewer.Graph.Controller.Runtime.Inputs
{
    public class GraphInput : IGraphInput, IScopeLifetimeListener, IUpdatable
    {
        public GraphInput(IUpdater updater, IInputView inputView, Controls.AssemblyGraphActions actions)
        {
            _updater = updater;
            _inputView = inputView;
            _actions = actions;
        }

        private readonly IUpdater _updater;
        private readonly IInputView _inputView;
        private readonly Controls.AssemblyGraphActions _actions;

        private Vector2 _moseMove;
        private float _mouseDelta;
        private float _mouseScroll;
        private bool _isRightMouseButton;

        private Vector2 _previousMousePosition;

        public Vector2 MouseMove => _moseMove;
        public float MouseDelta => _mouseDelta;
        public float MouseScroll => _mouseScroll;
        public bool IsRightMouseButton => _isRightMouseButton;

        public void OnUpdate(float _)
        {
            var mousePosition = Mouse.current.position.ReadValue();

            if (_previousMousePosition == Vector2.zero)
            {
                _previousMousePosition = mousePosition;
                return;
            }

            var move = mousePosition - _previousMousePosition;
            var delta = move.sqrMagnitude;
            _previousMousePosition = mousePosition;

            _moseMove = move;
            _mouseDelta = delta;
        }

        public void OnLifetimeCreated(ILifetime lifetime)
        {
          //  _inputView.ListenLifetime.View(lifetime, OnInputConstructed);
        }
        
        public void OnInputConstructed(IReadOnlyLifetime lifetime)
        {
            _actions.Scroll.Listen(lifetime, OnScrollPerformed, OnScrollCanceled);
            _actions.LeftMouseButton.Listen(lifetime, OnLeftMouseButtonPerformed, OnLeftMouseButtonCanceled);
            _actions.RightMouseButton.Listen(lifetime, OnRightMouseButtonPerformed, OnRightMouseButtonCanceled);
            
            _updater.Add(lifetime, this);
        }

        private void OnScrollPerformed(InputAction.CallbackContext context)
        {
            _mouseScroll = context.ReadValue<float>();
        }

        private void OnScrollCanceled(InputAction.CallbackContext context)
        {
            _mouseScroll = 0f;
        }

        private void OnLeftMouseButtonCanceled(InputAction.CallbackContext context)
        {
        }

        private void OnLeftMouseButtonPerformed(InputAction.CallbackContext context)
        {
        }

        private void OnRightMouseButtonPerformed(InputAction.CallbackContext context)
        {
            _isRightMouseButton = true;
        }

        private void OnRightMouseButtonCanceled(InputAction.CallbackContext context)
        {
            _isRightMouseButton = false;
        }
    }
}