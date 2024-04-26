using System.Collections.Generic;
using Global.Debugs.Drawing.Abstract;
using Shapes;
using Tools.AssembliesViewer.Graph.Connections.Abstract;
using Tools.AssembliesViewer.Graph.Nodes.Abstract;
using UnityEngine;

namespace Tools.AssembliesViewer.Graph.Connections.Runtime
{
    [DisallowMultipleComponent]
    public class NodeConnection : MonoBehaviour, INodeConnection
    {
        [SerializeField] private NodeConnectionConfig _config;
        [SerializeField] private Polyline _line;

        private IAssemblyNodeView _from;
        private IAssemblyNodeView _to;

        private bool _isInitialized;

        public void Construct(IAssemblyNodeView from, IAssemblyNodeView to)
        {
            _to = to;
            _from = from;
        }

        public void Initialize()
        {
            _isInitialized = true;
        }

        public void Draw(IShapeDrawer drawer)
        {
            if (_isInitialized == false)
                return;
            
            if (_from.IsActive == false || _to.IsActive == false)
            {
                Disable();
                return;
            }

            Enable();
            
            _line.points.Clear();
            _line.transform.localPosition = Vector3.zero;
            _line.Thickness = _config.Width;

            var direction = (_to.Position - _from.Position).normalized;
            var distance = Vector2.Distance(_to.Position, _from.Position);

            var target = direction * distance;
            var middle = Vector2.Lerp(Vector2.zero, target, 0.5f);
            var normalizedDistance = Mathf.Clamp(distance / _config.MaxDistance, 0f, 1f);
            var splineScale = _config.CurveScale.Evaluate(normalizedDistance);

            if (direction.x < 0)
                splineScale *= -1f;

            middle.x += _config.Down * splineScale;

            var points = SplineGenerator.GenerateSpline(
                new List<Vector2>() { Vector3.zero, middle, target }, _config.Steps, _config.Tension);

            var removeIndexes = new List<int>();

            for (var i = 1; i < points.Count; i++)
            {
                var checkDistance = Vector2.Distance(points[i - 1], points[i]);

                if (checkDistance < 1)
                    removeIndexes.Add(i);
            }

            var offset = 0;

            foreach (var removeIndex in removeIndexes)
            {
                points.RemoveAt(removeIndex - offset);
                offset++;
            }

            for (var i = 0; i < points.Count; i++)
            {
                var point = points[i];
                var color = Color.Lerp(_config.ToColor, _config.FromColor, i / (float)points.Count);
                _line.points.Add(new PolylinePoint(point, color));
            }

            _line.meshOutOfDate = true;
        }

        public void Enable()
        {
            gameObject.SetActive(true);
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }
    }
}