using Common.DataTypes.Runtime.Reactive;
using Global.UI.StateMachines.Abstract;

namespace Features.Menu.Loop.Abstract
{
    public interface IMenuMain : IUIState
    {
        IViewableDelegate PlaySwitchRequested { get; }
        IViewableDelegate OverlayOpened { get; }
        IViewableDelegate OverlayClosed { get; }
        
        void Enter();
    }
}