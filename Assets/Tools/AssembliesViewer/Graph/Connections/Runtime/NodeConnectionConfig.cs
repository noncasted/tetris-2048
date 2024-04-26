using NaughtyAttributes;
using Sirenix.OdinInspector;
using Tools.AssembliesViewer.Common;
using UnityEngine;

namespace Tools.AssembliesViewer.Graph.Connections.Runtime
{
    [InlineEditor]
    [CreateAssetMenu(fileName = "ConnectionConfig", menuName = AssemblyViewerRoutes.Root + "Config/Connection")]
    public class NodeConnectionConfig : ScriptableObject
    {
        [SerializeField] private Color _fromColor;
        [SerializeField] private Color _toColor;
        [SerializeField] private float _width;
        [SerializeField] private int _steps;
        [SerializeField] private float _tension;
        [SerializeField] private float _down;
        [SerializeField] [CurveRange(0f,0f,1f,1f)] private AnimationCurve _curveScale;
        [SerializeField] private float _maxDistance;

        public Color FromColor => _fromColor;
        public Color ToColor => _toColor;
        public float Width => _width;
        public int Steps => _steps;
        public float Tension => _tension;
        public float Down => _down;
        public AnimationCurve CurveScale => _curveScale;
        public float MaxDistance => _maxDistance;
    }
}