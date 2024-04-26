using Common.DataTypes.Runtime.Reactive;
using Internal.Scopes.Abstract.Lifetimes;

namespace Global.UI.StateMachines.Abstract
{
    public interface IStateHandle
    {
        IReadOnlyLifetime VisibilityLifetime { get; }
        IReadOnlyLifetime HierarchyLifetime { get; }
        IReadOnlyLifetime StackLifetime { get; }
        IViewableDelegate Recovered { get; }
        
        void Exit();
    }
}