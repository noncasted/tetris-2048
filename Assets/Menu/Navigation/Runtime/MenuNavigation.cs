﻿using Common.Components.Runtime.ObjectLifetime;
using Common.DataTypes.Runtime.Reactive;
using Global.UI.StateMachines.Abstract;
using Loop.Abstract;
using Menu.Common;
using Menu.Navigation.Abstract;
using UnityEngine;
using VContainer;

namespace Menu.Navigation.Runtime
{
    public class MenuNavigation : MonoBehaviour, IMenuNavigation
    {
        [SerializeField] private MenuPlayButton _playButton;
        [SerializeField] private MenuNavigationDictionary _entries;
        [SerializeField] private CanvasGroupUpdater _canvasGroup;

        private readonly ViewableDelegate<MenuNavigationTarget> _targetSelected = new();
        private IGameState _state;

        public IViewableDelegate<MenuNavigationTarget> TargetSelected => _targetSelected;
        public IUIConstraints Constraints => new UIConstraints();
        public string Name => "Navigation";

        [Inject]
        private void Construct(IGameState state)
        {
            _state = state;
        }

        private void OnEnable()
        {
            var lifetime = this.GetObjectLifetime();

            foreach (var (button, target) in _entries)
                button.Clicked.Listen(lifetime, () => _targetSelected.Invoke(target));
        }

        public void OnEntered(IStateHandle handle)
        {
            _canvasGroup.Show();
            handle.VisibilityLifetime.ListenTerminate(() =>
            {
                _canvasGroup.Hide();
                Debug.Log("Navigation visibility lifetime terminate");
            });
            
            handle.StackLifetime.ListenTerminate(() =>
            {
                Debug.Log("Navigation stack lifetime terminate");
            });
            _state.IsPaused.View(handle.VisibilityLifetime, isPaused =>
            {
                if (isPaused == true)
                    _playButton.SetToPlay();
                else
                    _playButton.SetToPause();
            });
        }
    }
}