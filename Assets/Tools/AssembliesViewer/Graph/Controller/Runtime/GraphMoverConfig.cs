using NaughtyAttributes;
using Sirenix.OdinInspector;
using Tools.AssembliesViewer.Common;
using UnityEngine;

namespace Tools.AssembliesViewer.Graph.Controller.Runtime
{
    [InlineEditor]
    [CreateAssetMenu(fileName = "GraphMoverConfig", menuName = AssemblyViewerRoutes.Root + "Config/Mover")]
    public class GraphMoverConfig : ScriptableObject
    {
        [SerializeField] [Min(0f)] private float _scrollSensitivity;
        [SerializeField] [Min(0f)] private float _scrollSpeed;
        [SerializeField] [Min(0f)] private float _moveSpeed;
        [SerializeField] [Min(0f)] private Vector2 _scaleBounds;
        [SerializeField] [Min(0f)] private float _nodeMoveSpeed;

        [SerializeField] [CurveRange(0f, 0f, 1f, 1f)]
        private AnimationCurve _scrollFactor;

        public float ScrollSensitivity => _scrollSensitivity;
        public float ScrollSpeed => _scrollSpeed;
        public float MoveSpeed => _moveSpeed;
        public AnimationCurve ScrollFactor => _scrollFactor;
        public Vector2 ScaleBounds => _scaleBounds;
        public float NodeMoveSpeed => _nodeMoveSpeed;
    }
}