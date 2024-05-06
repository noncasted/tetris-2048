using Common.DataTypes.Runtime.Reactive;
using Features.Loop.Abstract;

namespace Features.Loop.Runtime
{
    public class GameState : IGameState
    {
        private readonly ViewableProperty<GameStateType> _state = new(GameStateType.End);
        private readonly ViewableProperty<bool> _isPaused = new(false);
        private readonly ViewableProperty<GameSpeed> _speed = new(GameSpeed.Normal);

        public IViewableProperty<GameStateType> State => _state;
        public IViewableProperty<bool> IsPaused => _isPaused;
        public IViewableProperty<GameSpeed> Speed => _speed;

        public void Set(GameStateType state)
        {
            _state.Set(state);
        }

        public void SetSpeed(GameSpeed speed)
        {
            _speed.Set(speed);
        }

        public void SetPause(bool isPaused)
        {
            _isPaused.Set(isPaused);
        }
    }
}