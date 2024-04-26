using Tools.AssembliesViewer.Graph.View.Abstract;
using UnityEngine;

namespace Tools.AssembliesViewer.Graph.View.Runtime
{
    [DisallowMultipleComponent]
    public class NodesRootView : MonoBehaviour, INodesRootView
    {
        [SerializeField] private RectTransform _block;

        public Transform Transform => transform;
        public Vector2 Position => _block.anchoredPosition;
        
        public void SetPosition(Vector2 position)
        {
            _block.anchoredPosition = position;
        }
    }
}