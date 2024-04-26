using Common.DataTypes.Runtime.Reactive;

namespace Menu.Loop.Abstract
{
    public interface IMenuNavigation
    {
        IViewableDelegate<MenuNavigationTarget> TargetSelected { get; }

        void SetMainButtonToPlay();
        void SetMainButtonToPause();
    }
}