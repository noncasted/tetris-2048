using Common.DataTypes.Runtime.Reactive;

namespace Menu.Loop.Abstract
{
    public interface IMenuLoop
    {
        IViewableDelegate PlayRequested { get; }
        IViewableDelegate OverlayRequested { get; }
        
        void Enter();
        void OnGameEnded(int currentScore);
    }
}