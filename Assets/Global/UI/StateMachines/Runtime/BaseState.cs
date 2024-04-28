using Common.DataTypes.Runtime.Reactive;
using Global.UI.StateMachines.Abstract;
using Internal.Scopes.Abstract.Lifetimes;
using Internal.Scopes.Runtime.Lifetimes;

namespace Global.UI.StateMachines.Runtime
{
    public class BaseState : IInternalStateHandle, IUIState
    {
        public BaseState()
        {
            VisibilityLifetime = new Lifetime();
            HierarchyLifetime = new Lifetime();
            StackLifetime = new Lifetime();
            Recovered = new ViewableDelegate();
        }
        
        public IUIConstraints Constraints => new UIConstraints();
        public string Name => "Base";
        
        public IReadOnlyLifetime VisibilityLifetime { get; }
        public IReadOnlyLifetime HierarchyLifetime { get; }
        public IReadOnlyLifetime StackLifetime { get; }
        public IViewableDelegate Recovered { get; }
        public IUIState State { get; }

        public void OnEntered(IStateHandle handle)
        {
        }
        
        public void Exit()
        {
        }

        public void OnStacked(IInternalStateHandle head)
        {
        }

        public void ClearStack()
        {
        }
    }
}