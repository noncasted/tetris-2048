using Common.DataTypes.Runtime.Reactive;
using Global.UI.StateMachines.Abstract;

namespace Menu.GameEnds.Abstract
{
    public interface IMenuGameEnd : IUIState
    {
        IViewableDelegate ExitRequested { get; }
        
        void Show(IStateHandle handle, int currentScore);
    }
}