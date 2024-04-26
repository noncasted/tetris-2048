using Common.DataTypes.Runtime.Reactive;

//using Nova;

namespace Tools.AssembliesViewer.Graph.Tree
{
    public interface IGraphTreeEntry
    {
     //   UIBlock2D Block { get; }
        IViewableProperty<float> Size { get; }
    }
}