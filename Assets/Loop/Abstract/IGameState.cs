using Common.DataTypes.Runtime.Reactive;

namespace Loop.Abstract
{
    public interface IGameState
    {
        IViewableProperty<GameStateType> State { get; }

        void Set(GameStateType state);
    }
}