namespace Global.UI.StateMachines.Abstract
{
    public interface IUiStateMachine
    {
        IUIState Base { get; }
        
        IStateHandle EnterAsChild(IUIState parent, IUIState state);
        IStateHandle EnterAsStack(IUIState parent, IUIState state);
        void ClearStack(IUIState state);
        void Exit(IUIState state);
    }
}