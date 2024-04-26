using UnityEngine;

namespace Tools.AssembliesViewer.Graph.View.Abstract
{
    public interface IAssemblyGraphView
    {
        Vector2 Position { get; }
        float Scale { get; }

        void SetPosition(Vector2 position);
        void SetScale(float scale);
    }
}