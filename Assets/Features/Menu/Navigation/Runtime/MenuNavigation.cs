using Common.Components.Runtime.ObjectLifetime;
using Common.DataTypes.Runtime.Reactive;
using Features.Loop.Abstract;
using Features.Menu.Common;
using Features.Menu.Navigation.Abstract;
using Global.UI.StateMachines.Abstract;
using UnityEngine;
using VContainer;

namespace Features.Menu.Navigation.Runtime
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
            handle.VisibilityLifetime.ListenTerminate(_canvasGroup.Hide);

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