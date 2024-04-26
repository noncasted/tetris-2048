using Common.DataTypes.Runtime.Reactive;
using Internal.Scopes.Abstract.Lifetimes;

namespace Global.Inputs.View.Abstract
{
    public interface IInputView
    {
        IViewableProperty<ILifetime> ListenLifetime { get; }
    }
}