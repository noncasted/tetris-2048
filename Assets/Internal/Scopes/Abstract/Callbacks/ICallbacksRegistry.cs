using System;
using Cysharp.Threading.Tasks;

namespace Internal.Scopes.Abstract.Callbacks
{
    public interface ICallbacksRegistry : ICallbacksListener
    {
        void AddScopeCallback<T>(Action<T> invoker, CallbackStage stage, int order);
        void AddScopeAsyncCallback<T>(Func<T, UniTask> invoker, CallbackStage stage, int order);
        void AddCustomListener(ICallbacksListener callbackRegistry);
        
        UniTask RunConstruct();
    }
}