using Common.DataTypes.Runtime.Reactive;
using Loop.Abstract;

namespace Loop.Runtime
{
    public class GameState : IGameState
    {
        private readonly ViewableProperty<GameStateType> _state = new(GameStateType.Ended);

        public IViewableProperty<GameStateType> State => _state;

        public void Set(GameStateType state)
        {
            _state.Set(state);
        }
    }
}