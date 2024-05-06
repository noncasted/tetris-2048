using Common.DataTypes.Runtime.Reactive;
using Global.UI.StateMachines.Abstract;

namespace Features.Menu.Navigation.Abstract
{
    public interface IMenuNavigation : IUIState
    {
        IViewableDelegate<MenuNavigationTarget> TargetSelected { get; }
    }
}