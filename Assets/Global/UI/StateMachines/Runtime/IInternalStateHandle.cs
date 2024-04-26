using Global.UI.StateMachines.Abstract;

namespace Global.UI.StateMachines.Runtime
{
    public interface IInternalStateHandle : IStateHandle
    {
        void OnStacked(IInternalStateHandle head);
        void ClearStack();
    }
}