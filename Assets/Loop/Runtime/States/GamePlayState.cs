using System.Collections.Generic;
using Common.DataTypes.Runtime.Structs;
using Cysharp.Threading.Tasks;
using GamePlay.Input.Abstract;
using GamePlay.Loop.Abstract;
using GamePlay.Save.Abstract;
using Global.Publisher.Abstract.DataStorages;
using Global.System.Updaters.Abstract;
using Internal.Scopes.Abstract.Lifetimes;
using Loop.Abstract;
using Menu.Loop.Abstract;

namespace Loop.Runtime.States
{
    public class GamePlayState : ILoopState
    {
        public GamePlayState(
            IGamePlayLoop gamePlay,
            IMenuMain menu,
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
        private readonly IMenuMain _menu;
        private readonly IUpdateSpeedSetter _updateSpeedSetter;
        private readonly IDataStorage _dataStorage;
        private readonly IGameState _state;
        private readonly IGameInput _input;
        private readonly IGameSaver _gameSaver;
        private readonly GameSpeedConfig _config;

        private bool _isOverlayed;

        public async UniTask<GameStateType> Enter(IReadOnlyLifetime stateLifetime)
        {
            _menu.Enter();

            _menu.PlaySwitchRequested.Listen(stateLifetime, OnPlaySwitchRequested);
            _menu.OverlayOpened.Listen(stateLifetime, OnOverlayOpened);
            _menu.OverlayClosed.Listen(stateLifetime, OnOverlayClosed);
            _state.Speed.View(stateLifetime, OnSpeedChanged);
            _input.Swipe.Listen(stateLifetime, OnInput);

            _state.Set(GameStateType.Game);
            _state.SetPause(true);
            _updateSpeedSetter.Set(0);

            var save = await _dataStorage.GetEntry<GameSave>();
            _gamePlay.Construct(stateLifetime, save);

            var result = await _gamePlay.HandleGame(stateLifetime);

            _gameSaver.ForceSave(new List<BoardStateBlock>());

            return GameStateType.End;
        }

        private void OnSpeedChanged(IReadOnlyLifetime _, GameSpeed speed)
        {
            if (_state.IsPaused.Value == true)
                return;

            _updateSpeedSetter.Set(_config.Speeds[speed]);
        }

        private void OnPlaySwitchRequested()
        {
            _state.SetPause(!_state.IsPaused.Value);

            if (_state.IsPaused.Value == true)
                _updateSpeedSetter.Set(_config.Speeds[_state.Speed.Value]);
        }

        private void OnOverlayOpened()
        {
            _isOverlayed = true;
            _state.SetPause(true);
            _updateSpeedSetter.Pause();
        }

        private void OnOverlayClosed()
        {
            _state.SetPause(true);
            _isOverlayed = false;
        }

        private void OnInput(CoordinateDirection direction)
        {
            if (_isOverlayed == true)
                return;

            if (_state.IsPaused.Value == true)
                _state.SetPause(false);
            
            _updateSpeedSetter.Set(_config.Speeds[_state.Speed.Value]);
            _gamePlay.PassInput(direction);
        }
    }
}