using Common.Components.Runtime.ObjectLifetime;
using Common.DataTypes.Runtime.Reactive;
using Menu.Loop.Abstract;
using UnityEngine;

namespace Menu.Loop.Runtime
{
    public class MenuNavigation : MonoBehaviour, IMenuNavigation
    {
        [SerializeField] private MenuPlayButton _playButton;
        [SerializeField] private MenuNavigationDictionary _entries;

        private readonly ViewableDelegate<MenuNavigationTarget> _targetSelected = new();

        public IViewableDelegate<MenuNavigationTarget> TargetSelected => _targetSelected;
        
        private void OnEnable()
        {
            var lifetime = this.GetObjectLifetime();

            foreach (var (button, target) in _entries)
                button.Clicked.Listen(lifetime, () => _targetSelected.Invoke(target));
        }
        
        public void SetMainButtonToPlay()
        {
            _playButton.SetToPlay();
        }

        public void SetMainButtonToPause()
        {
            _playButton.SetToPause();
        }
    }
}