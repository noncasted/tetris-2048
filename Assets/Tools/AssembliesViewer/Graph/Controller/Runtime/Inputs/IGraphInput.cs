using UnityEngine;

namespace Tools.AssembliesViewer.Graph.Controller.Runtime.Inputs
{
    public interface IGraphInput
    {
        Vector2 MouseMove { get; }
        float MouseDelta { get; }
        float MouseScroll { get; }
        bool IsRightMouseButton { get; }
    }
}