using Tools.AssembliesViewer.Domain.Abstract;
using Tools.AssembliesViewer.Graph.Nodes.Abstract;

namespace Tools.AssembliesViewer.Graph.Controller.Abstract
{
    public interface IGraphControllerInterceptor
    {
        void SavePositions();
        void Redraw();
        void DisableAssembly(IAssembly assembly);
        void EnableAssembly(IAssembly assembly);
        void Select(IAssemblyNodeView view);
    }
}