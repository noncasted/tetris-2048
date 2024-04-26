using Tools.AssembliesViewer.Domain.Abstract;
using UnityEngine;

namespace Tools.AssembliesViewer.Graph.Nodes.Abstract
{
    public interface IAssemblyNodeView
    {
        Transform Transform { get; }
        Vector2 Position { get; }
        Vector3 WorldPosition { get; }
        bool IsActive { get; }
        bool IsPressed { get; }
        bool IsDirty { get; }
        IAssembly Assembly { get; }

        void Enable();
        void Disable();
        void SetPosition(Vector2 position);
        void OnMoved();
        void ResetSelection();
    }
}