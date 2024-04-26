using Common.DataTypes.Runtime.Reactive;
using Internal.Scopes.Abstract.Lifetimes;

namespace Global.UI.Design.Abstract.Elements
{
    public interface IDesignElement
    {
        IViewableProperty<DesignElementState> State { get; }
        IReadOnlyLifetime Lifetime { get; }

        void SetState(DesignElementState state);
    }
}