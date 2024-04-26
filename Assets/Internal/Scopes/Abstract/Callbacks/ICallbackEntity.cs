using Cysharp.Threading.Tasks;

namespace Internal.Scopes.Abstract.Callbacks
{
    public interface ICallbackEntity
    {
        int Order { get; }

        void Listen(object target);
        UniTask InvokeAsync();
    }
}