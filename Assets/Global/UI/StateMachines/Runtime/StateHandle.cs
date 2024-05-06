using Common.DataTypes.Runtime.Reactive;
using Global.Inputs.Constraints.Abstract;
using Global.UI.StateMachines.Abstract;
using Internal.Scopes.Abstract.Lifetimes;

namespace Global.UI.StateMachines.Runtime
{
    public class StateHandle : IInternalStateHandle
    {
        public StateHandle(IInternalStateHandle parent, IUIState state, IInputConstraintsStorage constraintsStorage)
        {
            _parent = parent;
            _state = state;
            _constraintsStorage = constraintsStorage;
            _hierarchyLifetime = parent.HierarchyLifetime.CreateChild();
            _stackLifetime = _hierarchyLifetime.CreateChild();
        }

        private readonly IInternalStateHandle _parent;
        private readonly IUIState _state;
        private readonly IInputConstraintsStorage _constraintsStorage;

        private readonly ViewableDelegate _recovered = new();

        private readonly ILifetime _hierarchyLifetime;
        private ILifetime _visibilityLifetime;
        private ILifetime _stackLifetime;

        private StateHandle _stack;

        public IReadOnlyLifetime VisibilityLifetime => _visibilityLifetime;
        public IReadOnlyLifetime HierarchyLifetime => _hierarchyLifetime;
        public IReadOnlyLifetime StackLifetime => _stackLifetime;
        public IViewableDelegate Recovered => _recovered;
        public IUIState State => _state;

        public void EnterAsChild()
        {
            _visibilityLifetime = _parent.VisibilityLifetime.CreateChild();
            _parent.Recovered.Listen(_hierarchyLifetime, Recover);
            
            _constraintsStorage.Add(_state.Constraints.Input);
            _state.OnEntered(this);
        }

        public void EnterAsStack()
        {
            _constraintsStorage.Add(_state.Constraints.Input);
            _parent.OnStacked(this);
            
            _parent.StackLifetime.ListenTerminate(_hierarchyLifetime.Terminate);
            _visibilityLifetime = _hierarchyLifetime.CreateChild();

            _state.OnEntered(this);
        }

        /// <summary>
        /// Notify parent state that it was stacked.
        /// Parent listens head's selfLifetime, so when head terminated, parent will be recovered
        /// </summary>
        /// <param name="head">State that stacks on current state</param>
        public void OnStacked(IInternalStateHandle head)
        {
            _stackLifetime?.Terminate();
            _stackLifetime = _hierarchyLifetime.CreateChild();
            
            _constraintsStorage.Remove(_state.Constraints.Input);
            _visibilityLifetime.Terminate();
            head.HierarchyLifetime.ListenTerminate(Recover);
        }

        public void ClearStack()
        {
            _stackLifetime?.Terminate();
            _stackLifetime = _hierarchyLifetime.CreateChild();
        }

        public void Recover()
        {
            _constraintsStorage.Add(_state.Constraints.Input);
            _visibilityLifetime = _parent.VisibilityLifetime.CreateChild();
            _state.OnEntered(this);
            _recovered.Invoke();
        }

        public void Exit()
        {
            _constraintsStorage.Remove(_state.Constraints.Input);
            _hierarchyLifetime.Terminate();
            _visibilityLifetime.Terminate();
        }
    }
}