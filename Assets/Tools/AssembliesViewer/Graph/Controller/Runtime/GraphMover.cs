using Global.System.Updaters.Abstract;
using Tools.AssembliesViewer.Graph.Controller.Runtime.Inputs;
using Tools.AssembliesViewer.Graph.Nodes.Abstract;
using Tools.AssembliesViewer.Graph.View.Abstract;
using UnityEngine;

namespace Tools.AssembliesViewer.Graph.Controller.Runtime
{
    public class GraphMover : IUpdatable
    {
        public GraphMover(
            IUpdater updater,
            IGraphInput input,
            IAssemblyGraphView view,
            INodesRootView nodesView,
            GraphMoverConfig config)
        {
            _updater = updater;
            _input = input;
            _view = view;
            _nodesView = nodesView;
            _config = config;
        }

        private readonly IUpdater _updater;
        private readonly IGraphInput _input;
        private readonly IAssemblyGraphView _view;
        private readonly INodesRootView _nodesView;
        private readonly GraphMoverConfig _config;

        private Vector2 _previousPosition;
        private float _targetScale;
        private IAssemblyNodeView _node;

        public void Start()
        {
            _updater.Add(this);
        }

        public void OnSelected(IAssemblyNodeView node)
        {
            _node = node;
        }

        public void OnUpdate(float delta)
        {
            if (_node != null)
            {
                if (_node.IsPressed == true)
                {
                    var scale = _view.Scale;
                    var position = _node.Position + _input.MouseMove * _config.NodeMoveSpeed * (1f / scale);
                    _node.SetPosition(position);
                }
                else
                {
                    _node.OnMoved();
                }
            }

            var scroll = _input.MouseScroll;
            _targetScale += scroll * _config.ScrollSensitivity;
            _targetScale = Mathf.Clamp(_targetScale, _config.ScaleBounds.x, _config.ScaleBounds.y);

            var resultScale = Mathf.Lerp(
                _view.Scale,
                _targetScale,
                delta * _config.ScrollSpeed);

            _view.SetScale(resultScale);

            if (_input.IsRightMouseButton)
            {
                if (_previousPosition == Vector2.zero)
                {
                    _previousPosition = Input.mousePosition;
                    return;
                }

                var mousePosition = (Vector2)Input.mousePosition;
                var direction = mousePosition - _previousPosition;
                var scrollFactor = _config.ScrollFactor.Evaluate(_targetScale / _config.ScaleBounds.y);
                var move = direction * _config.MoveSpeed * scrollFactor * Time.deltaTime;
                _nodesView.SetPosition(_nodesView.Position + move);
                _previousPosition = mousePosition;
            }
            else
            {
                _previousPosition = Vector3.zero;
            }
        }
    }
}