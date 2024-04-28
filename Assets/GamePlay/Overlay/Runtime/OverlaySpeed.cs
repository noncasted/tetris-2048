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
        
        private IGameState _gameState;

        [Inject]
        private void Construct(IGameState gameState)
        {
            _gameState = gameState;
            _speedSelection.MoveTo(_buttons[gameState.Speed.Value].Target);
        }

        private void OnEnable()
        {
            var lifetime = this.GetObjectLifetime();

            foreach (var (speed, button) in _buttons)
                button.Button.Clicked.Listen(lifetime, () => OnClicked(button.Target, speed));
        }

        private void OnClicked(Transform target, GameSpeed speed)
        {
            _gameState.SetSpeed(speed);
            _speedSelection.MoveTo(target);
        }
    }
}