using System.Collections.Generic;
using Global.Inputs.Constraints.Abstract;
using Global.UI.StateMachines.Abstract;

namespace Global.UI.StateMachines.Runtime
{
    public class UiStateMachine : IUiStateMachine
    {
        public UiStateMachine(IInputConstraintsStorage constraintsStorage)
        {
            _constraintsStorage = constraintsStorage;

            var state = new BaseState();
            Base = state;
            
            _handles = new Dictionary<IUIState, IInternalStateHandle>()
            {
                { state, state }
            };
        }   

        private readonly IInputConstraintsStorage _constraintsStorage;

        private readonly Dictionary<IUIState, IInternalStateHandle> _handles;

        public IUIState Base { get; }

        public IStateHandle EnterAsChild(IUIState parent, IUIState state)
        {
            var headHandle = _handles[parent];

            var childHandle = new StateHandle(headHandle, state, _constraintsStorage);
            _handles[state] = childHandle;
            childHandle.EnterAsChild();

            return childHandle;
        }

        public IStateHandle EnterAsStack(IUIState parent, IUIState state)
        {
            var headHandle = _handles[parent];

            var childHandle = new StateHandle(headHandle, state, _constraintsStorage);
            _handles[state] = childHandle;
            childHandle.EnterAsStack();
            
            return childHandle;
        }

        public void ClearStack(IUIState state)
        {
            _handles[state].ClearStack();
        }

        public void Exit(IUIState state)
        {
            _handles[state].Exit();
        }
    }
}