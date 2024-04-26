using System;
using System.Collections.Generic;
using Common.DataTypes.Runtime.Structs;
using Cysharp.Threading.Tasks;
using GamePlay.Input.Abstract;
using GamePlay.Loop.Abstract;
using GamePlay.Save.Abstract;
using Global.Publisher.Abstract.DataStorages;
using Global.System.Updaters.Abstract;
using Internal.Scopes.Abstract.Instances.Services;
using Internal.Scopes.Abstract.Lifetimes;
using Loop.Abstract;
using Menu.Loop.Abstract;

namespace Loop.Runtime
{
    public class GameLoop : IScopeLoadAsyncListener, IScopeLifetimeListener, IGameLoop
    {
        public GameLoop(
            IGamePlayLoop gamePlay,
            IMenuLoop menu,
            IUpdateSpeedSetter updateSpeedSetter,
            IDataStorage dataStorage,
            IGameState state,
            IGameInput input,
            IGameSaver gameSaver,
            GameSpeedConfig config)
        {
            _gamePlay = gamePlay;
            _menu = menu;
            _updateSpeedSetter = updateSpeedSetter;
            _dataStorage = dataStorage;
            _state = state;
            _input = input;
            _gameSaver = gameSaver;
            _config = config;
        }

        private readonly IGamePlayLoop _gamePlay;
        private readonly IMenuLoop _menu;
        private readonly IUpdateSpeedSetter _updateSpeedSetter;
        private readonly IDataStorage _dataStorage;
        private readonly IGameState _state;
        private readonly IGameInput _input;
        private readonly IGameSaver _gameSaver;

        private readonly GameSpeedConfig _config;

        private ILifetime _gamePlayLifetime;
        private ILifetime _scopeLifetime;

        private GameSpeed _speed = GameSpeed.Normal;

        public GameSpeed Speed => _speed;

        public void OnLifetimeCreated(ILifetime lifetime)
        {
            _scopeLifetime = lifetime;
            _menu.PlayRequested.Listen(lifetime, OnPlayRequested);
            _menu.OverlayRequested.Listen(lifetime, OnOverlayRequested);
            _input.Swipe.Listen(lifetime, OnInput);
        }

        public async UniTask OnLoadedAsync()
        {
            _menu.Enter();
            _state.Set(GameStateType.Paused);

            Restart().Forget();
            _updateSpeedSetter.Pause();
        }

        private async UniTask Restart()
        {
            _gamePlayLifetime?.Terminate();
            _gamePlayLifetime = _scopeLifetime.CreateChild();

            var save = await _dataStorage.GetEntry<GameSave>();
            _gamePlay.Construct(_gamePlayLifetime, save);

            var result = await _gamePlay.HandleGame(_gamePlayLifetime);

            _gamePlayLifetime?.Terminate();
            _state.Set(GameStateType.Ended);
            _menu.OnGameEnded(result.Score);

            _gameSaver.ForceSave(new List<BoardStateBlock>());
        }

        private void OnPlayRequested()
        {
            switch (_state.State.Value)
            {
                case GameStateType.Ended:
                    _state.Set(GameStateType.InGame);
                    Restart().Forget();
                    break;
                case GameStateType.InGame:
                    Pause();
                    _state.Set(GameStateType.Paused);
                    break;
                case GameStateType.Paused:
                    Continue();
                    _state.Set(GameStateType.InGame);
                    break;
                case GameStateType.Menu:
                    Continue();
                    _state.Set(GameStateType.InGame);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void OnOverlayRequested()
        {
            if (_state.State.Value == GameStateType.Menu || _state.State.Value == GameStateType.Ended)
                return;

            Pause();
            _state.Set(GameStateType.Menu);
        }

        private void OnInput(CoordinateDirection direction)
        {
            if (_state.State.Value != GameStateType.Paused && _state.State.Value != GameStateType.InGame)
                return;

            if (_state.State.Value != GameStateType.InGame)
            {
                Continue();
                _state.Set(GameStateType.InGame);
            }

            _gamePlay.PassInput(direction);
        }

        public void SetGameSpeed(GameSpeed speed)
        {
            _speed = speed;
            
            if (_state.State.Value != GameStateType.InGame)
                return;
            
            _updateSpeedSetter.Set(_config.Speeds[speed]);
        }

        private void Pause()
        {
            if (_state.State.Value != GameStateType.InGame)
                return;

            _updateSpeedSetter.Pause();
        }

        private void Continue()
        {
            if (_state.State.Value == GameStateType.InGame)
                return;

            _updateSpeedSetter.Continue();
            _updateSpeedSetter.Set(_config.Speeds[_speed]);
        }
    }
}