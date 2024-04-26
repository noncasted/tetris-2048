using Global.UI.StateMachines.Abstract;

namespace Menu.GameEnds.Abstract
{
    public interface IMenuGameEnd : IUIState
    {
        void Show(IStateHandle handle, int currentScore);
    }
}