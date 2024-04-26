using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Internal.Scopes.Abstract.Callbacks;
using Internal.Scopes.Abstract.Instances.Entities;
using Internal.Scopes.Abstract.Lifetimes;
using Internal.Scopes.Runtime.Callbacks;

namespace Internal.Scopes.Common.Entity
{
    public class EntityDefaultCallbacksRegister : IEntityCallbacks, ICallbacksListener
    {
        private readonly ICallbacksRegistry _callbacksRegistry;
        private readonly IScopedEntityUtils _utils;

        private readonly Dictionary<EntityCallbackStage, ICallbacksStage> _stages = new();
        private readonly List<IEntitySwitchLifetimeListener> _switchLifetimeListeners = new();

        public EntityDefaultCallbacksRegister(ICallbacksRegistry callbacksRegistry, IScopedEntityUtils utils)
        {
            _callbacksRegistry = callbacksRegistry;
            _utils = utils;
        }
        
        public void AddCallbacks()
        {
            _callbacksRegistry.AddCustomListener(this);

            _stages.Add(EntityCallbackStage.Enable, new CallbacksHandler());
            _stages.Add(EntityCallbackStage.Disable, new CallbacksHandler());
            
            AddConstruct<IEntityAwakeListener>(listener => listener.OnAwake(), 0);
            AddAsyncConstruct<IEntityAwakeAsyncListener>(listener => listener.OnAwakeAsync(), 1000);
            AddConstruct<IEntityLifetimeListener>(listener => listener.OnLifetimeCreated(_utils.Lifetime), 3000);

            AddEnable<IEntityEnableListener>(listener => listener.OnEnabled(), 0);
            AddAsyncEnable<IEntityEnableAsyncListener>(listener => listener.OnEnabledAsync(), 1000);

            AddDisable<IEntityDisableListener>(listener => listener.OnDisabled(), 0);
            AddAsyncDisable<IEntityDisableAsyncListener>(listener => listener.OnDisabledAsync(), 1000);

            AddDispose<IEntityDestroyListener>(listener => listener.OnDestroyed(), 0);
            AddAsyncDispose<IEntityDestroyAsyncListener>(listener => listener.OnDestroyedAsync(), 1000);

            return;

            void AddConstruct<T>(Action<T> invoker, int order)
            {
                _callbacksRegistry.AddScopeCallback(invoker, CallbackStage.Construct, order);
            }

            void AddAsyncConstruct<T>(Func<T, UniTask> invoker, int order)
            {
                _callbacksRegistry.AddScopeAsyncCallback(invoker, CallbackStage.Construct, order);
            }

            void AddEnable<T>(Action<T> invoker, int order)
            {
                var entity = new CallbackEntity<T>(invoker, order);
                _stages[EntityCallbackStage.Enable].Add(entity);
            }

            void AddAsyncEnable<T>(Func<T, UniTask> invoker, int order)
            {
                var entity = new AsyncCallbackEntity<T>(invoker, order);
                _stages[EntityCallbackStage.Enable].Add(entity);
            }

            void AddDisable<T>(Action<T> invoker, int order)
            {
                var entity = new CallbackEntity<T>(invoker, order);
                _stages[EntityCallbackStage.Disable].Add(entity);
            }

            void AddAsyncDisable<T>(Func<T, UniTask> invoker, int order)
            {
                var entity = new AsyncCallbackEntity<T>(invoker, order);
                _stages[EntityCallbackStage.Disable].Add(entity);
            }

            void AddDispose<T>(Action<T> invoker, int order)
            {
                _callbacksRegistry.AddScopeCallback(invoker, CallbackStage.Dispose, order);
            }

            void AddAsyncDispose<T>(Func<T, UniTask> invoker, int order)
            {
                _callbacksRegistry.AddScopeAsyncCallback(invoker, CallbackStage.Dispose, order);
            }
        }
        
        public void Listen(object listener)
        {
            if (listener is IEntitySwitchLifetimeListener switchLifetimeListener)
                _switchLifetimeListeners.Add(switchLifetimeListener);
        }


        public UniTask RunConstruct()
        {
            return _callbacksRegistry.RunConstruct();
        }

        public UniTask RunEnable(ILifetime lifetime)
        {
            foreach (var listener in _switchLifetimeListeners)
                listener.OnSwitchLifetimeCreated(lifetime);
            
            return _stages[EntityCallbackStage.Enable].Run();
        }

        public UniTask RunDisable()
        {
            return _stages[EntityCallbackStage.Disable].Run();
        }
    }
}