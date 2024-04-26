using Common.Components.Runtime.ObjectLifetime;
using Loop.Abstract;
using UnityEngine;
using VContainer;

namespace GamePlay.Overlay.Runtime
{
    [DisallowMultipleComponent]
    public class OverlaySpeed : MonoBehaviour
    {
        [SerializeField] private OverlaySpeedSelection _speedSelection;
        [SerializeField] private OverlaySpeedButtonsDictionary _buttons;

        private IGameLoop _gameLoop;

        [Inject]
        private void Construct(IGameLoop gameLoop)
        {
            _gameLoop = gameLoop;
            _speedSelection.MoveTo(_buttons[_gameLoop.Speed].Target);
        }

        private void OnEnable()
        {
            var lifetime = this.GetObjectLifetime();

            foreach (var (speed, button) in _buttons)
                button.Button.Clicked.Listen(lifetime, () => OnClicked(button.Target, speed));
        }

        private void OnClicked(Transform target, GameSpeed speed)
        {
            _gameLoop.SetGameSpeed(speed);
            _speedSelection.MoveTo(target);
        }
    }
}