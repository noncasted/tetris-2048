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
            _visibility = new Lifetime();
            _stack = new Lifetime();
            HierarchyLifetime = new Lifetime();
            Recovered = new ViewableDelegate();
        }
        
        public IUIConstraints Constraints => new UIConstraints();
        public string Name => "Base";

        private Lifetime _visibility;
        private Lifetime _stack;

        public IReadOnlyLifetime VisibilityLifetime => _visibility;
        public IReadOnlyLifetime HierarchyLifetime { get; }
        public IReadOnlyLifetime StackLifetime => _stack;
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
            _stack.Terminate();
            _visibility.Terminate();

            _stack = new Lifetime();
            _visibility = new Lifetime();
        }

        public void ClearStack()
        {
        }
    }
}