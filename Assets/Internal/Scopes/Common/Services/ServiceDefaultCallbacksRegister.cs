using System;
using Cysharp.Threading.Tasks;
using Internal.Scopes.Abstract.Callbacks;
using Internal.Scopes.Abstract.Instances.Services;

namespace Internal.Scopes.Common.Services
{
    public class ServiceDefaultCallbacksRegister
    {
        public void AddCallbacks(ICallbacksRegistry callbacks, IServiceScopeData data)
        {
            AddConstruct<IScopeBuiltListener>(listener => listener.OnContainerBuilt(data.Scope), 0);
            AddConstruct<IScopeAwakeListener>(listener => listener.OnAwake(), 1000);
            AddAsyncConstruct<IScopeAwakeAsyncListener>(listener => listener.OnAwakeAsync(), 2000);

            AddConstruct<IScopeLifetimeListener>(listener => listener.OnLifetimeCreated(data.Lifetime), 0);
            AddConstruct<IScopeEnableListener>(listener => listener.OnEnabled(), 1000);
            AddAsyncConstruct<IScopeEnableAsyncListener>(listener => listener.OnEnabledAsync(), 2000);
            AddConstruct<IScopeLoadListener>(listener => listener.OnLoaded(), 3000);
            AddAsyncConstruct<IScopeLoadAsyncListener>(listener => listener.OnLoadedAsync(), 4000);

            AddDispose<IScopeDisableListener>(listener => listener.OnDisabled(), 0);
            AddAsyncDispose<IScopeDisableAsyncListener>(listener => listener.OnDisabledAsync(), 1000);

            return;

            void AddConstruct<T>(Action<T> invoker, int order)
            {
                callbacks.AddScopeCallback(invoker, CallbackStage.Construct, order);
            }

            void AddAsyncConstruct<T>(Func<T, UniTask> invoker, int order)
            {
                callbacks.AddScopeAsyncCallback(invoker, CallbackStage.Construct, order);
            }

            void AddDispose<T>(Action<T> invoker, int order)
            {
                callbacks.AddScopeCallback(invoker, CallbackStage.Dispose, order);
            }

            void AddAsyncDispose<T>(Func<T, UniTask> invoker, int order)
            {
                callbacks.AddScopeAsyncCallback(invoker, CallbackStage.Dispose, order);
            }
        }
    }
}