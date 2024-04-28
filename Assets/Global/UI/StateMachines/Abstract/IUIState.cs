namespace Global.UI.StateMachines.Abstract
{
    public interface IUIState
    {
        IUIConstraints Constraints { get; }
        string Name { get; }

        void OnEntered(IStateHandle handle);
    }
}