using System;
using Common.DataTypes.Runtime.Reactive;
using Global.UI.StateMachines.Abstract;
using Internal.Scopes.Abstract.Instances.Services;
using Internal.Scopes.Abstract.Lifetimes;
using Loop.Abstract;
using Menu.GameEnds.Abstract;
using Menu.Leaderboards.Abstract;
using Menu.Loop.Abstract;
using Menu.Main.Abstract;
using Menu.Settings.Abstract;

namespace Menu.Loop.Runtime
{
    public class MenuLoop : IMenuLoop, IScopeLifetimeListener
    {
        public MenuLoop(
            IUiStateMachine stateMachine,
            IGameState gameState,
            IMenuMain main,
            IMenuSettings settings,
            IMenuGameEnd gameEnd,
            IMenuAbout about,
            IMenuNavigation navigation)
        {
            _stateMachine = stateMachine;
            _gameState = gameState;
            _main = main;
            _settings = settings;
            _gameEnd = gameEnd;
            _about = about;
            _navigation = navigation;
        }

        private readonly IUiStateMachine _stateMachine;
        private readonly IGameState _gameState;
        private readonly IMenuMain _main;
        private readonly IMenuSettings _settings;
        private readonly IMenuGameEnd _gameEnd;
        private readonly IMenuAbout _about;
        private readonly IMenuNavigation _navigation;

        private readonly ViewableDelegate _playRequested = new();
        private readonly ViewableDelegate _overlayRequested = new();

        private MenuNavigationTarget _currentTarget = MenuNavigationTarget.None;

        public IViewableDelegate PlayRequested => _playRequested;
        public IViewableDelegate OverlayRequested => _overlayRequested;

        public void OnLifetimeCreated(ILifetime lifetime)
        {
            _navigation.TargetSelected.Listen(lifetime, OnTargetSelected);
            _gameState.State.View(lifetime, OnGameStateChanged);
        }

        public void Enter()
        {
            _stateMachine.EnterAsChild(_stateMachine.Base, _main);
        }

        public void OnGameEnded(int currentScore)
        {
            var handle = _stateMachine.EnterAsStack(_main, _gameEnd);
            _gameEnd.Show(handle, currentScore);
        }

        private void OnTargetSelected(MenuNavigationTarget target)
        {
            if (_currentTarget == target && target != MenuNavigationTarget.Play)
                return;

            _currentTarget = target;

            switch (target)
            {
                case MenuNavigationTarget.Settings:
                    _navigation.SetMainButtonToPlay();
                    _stateMachine.EnterAsStack(_main, _settings);
                    _overlayRequested.Invoke();
                    break;
                case MenuNavigationTarget.Play:
                    _stateMachine.ClearStack(_main);
                    _playRequested?.Invoke();
                    break;
                case MenuNavigationTarget.Leaderboards:
                    _navigation.SetMainButtonToPlay();
                    _stateMachine.EnterAsStack(_main, _about);
                    _overlayRequested.Invoke();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(target), target, null);
            }
        }

        private void OnGameStateChanged(IReadOnlyLifetime lifetime, GameStateType state)
        {
            switch (state)
            {
                case GameStateType.Ended:
                    _navigation.SetMainButtonToPlay();
                    break;
                case GameStateType.InGame:
                    _navigation.SetMainButtonToPause();
                    break;
                case GameStateType.Paused:
                    _navigation.SetMainButtonToPlay();
                    break;
                case GameStateType.Menu:
                    _navigation.SetMainButtonToPlay();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }
    }
}