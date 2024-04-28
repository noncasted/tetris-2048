using Common.DataTypes.Runtime.Reactive;

namespace Loop.Abstract
{
    public interface IGameState
    {
        IViewableProperty<GameStateType> State { get; }
        IViewableProperty<bool> IsPaused { get; }
        IViewableProperty<GameSpeed> Speed { get; }

        void Set(GameStateType state);
        void SetSpeed(GameSpeed speed);
        void SetPause(bool isPaused);
    }
}