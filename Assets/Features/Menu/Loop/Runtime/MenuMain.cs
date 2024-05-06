using System;
using Common.DataTypes.Runtime.Reactive;
using Features.Loop.Abstract;
using Features.Menu.Leaderboards.Abstract;
using Features.Menu.Loop.Abstract;
using Features.Menu.Navigation.Abstract;
using Features.Menu.Settings.Abstract;
using Global.UI.StateMachines.Abstract;
using Internal.Scopes.Abstract.Instances.Services;
using Internal.Scopes.Abstract.Lifetimes;

namespace Features.Menu.Loop.Runtime
{
    public class MenuMain : IMenuMain, IScopeLifetimeListener
    {
        public MenuMain(
            IUiStateMachine stateMachine,
            IGameState gameState,
            IMenuSettings settings,
            IMenuAbout about,
            IMenuNavigation navigation)
        {
            _stateMachine = stateMachine;
            _gameState = gameState;
            _settings = settings;
            _about = about;
            _navigation = navigation;
        }

        private readonly IUiStateMachine _stateMachine;
        private readonly IGameState _gameState;
        private readonly IMenuSettings _settings;
        private readonly IMenuAbout _about;
        private readonly IMenuNavigation _navigation;

        private readonly ViewableDelegate _playRequested = new();
        private readonly ViewableDelegate _overlayOpened = new();
        private readonly ViewableDelegate _overlayClosed = new();

        private IStateHandle _current;

        public IViewableDelegate PlaySwitchRequested => _playRequested;
        public IViewableDelegate OverlayOpened => _overlayOpened;
        public IViewableDelegate OverlayClosed => _overlayClosed;

        public IUIConstraints Constraints => new UIConstraints();
        public string Name => "Main";

        public void OnLifetimeCreated(ILifetime lifetime)
        {
            _navigation.TargetSelected.Listen(lifetime, OnTargetSelected);
        }

        public void Enter()
        {
            var handle = _stateMachine.EnterAsStack(_stateMachine.Base, this);
            _stateMachine.EnterAsChild(this, _navigation);
        }

        public void OnEntered(IStateHandle handle)
        {
        }

        private void OnTargetSelected(MenuNavigationTarget target)
        {
            switch (target)
            {
                case MenuNavigationTarget.Settings:
                    SwitchState(_settings);
                    break;
                case MenuNavigationTarget.Play:
                    if (_current != null)
                    {
                        _current.Exit();
                        _current = null;
                        _overlayClosed.Invoke();
                    }

                    _playRequested?.Invoke();
                    break;
                case MenuNavigationTarget.About:
                    SwitchState(_about);
                    break;
                case MenuNavigationTarget.None:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(target), target, null);
            }

            return;

            void SwitchState(IUIState state)
            {
                if (_current == null)
                {
                    _current = _stateMachine.EnterAsChild(this, state);
                    _overlayOpened.Invoke();
                    return;
                }

                if (_current.State == state)
                {
                    _current.Exit();
                    _current = null;
                    _overlayClosed.Invoke();
                    return;
                }

                _current.Exit();
                _current = _stateMachine.EnterAsChild(this, state);
            }
        }
    }
}