using Common.DataTypes.Runtime.Reactive;
using Global.UI.StateMachines.Abstract;

namespace Menu.Navigation.Abstract
{
    public interface IMenuNavigation : IUIState
    {
        IViewableDelegate<MenuNavigationTarget> TargetSelected { get; }
    }
}