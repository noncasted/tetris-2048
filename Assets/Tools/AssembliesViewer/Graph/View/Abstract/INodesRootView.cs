using UnityEngine;

namespace Tools.AssembliesViewer.Graph.View.Abstract
{
    public interface INodesRootView
    {
        Transform Transform { get; }
        Vector2 Position { get; }

        void SetPosition(Vector2 position);
    }
}