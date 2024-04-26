using Global.Debugs.Drawing.Abstract;

namespace Tools.AssembliesViewer.Graph.Connections.Abstract
{
    public interface INodeConnection
    {
        void Draw(IShapeDrawer drawer);
        void Initialize();
        void Enable();
        void Disable();
    }
}