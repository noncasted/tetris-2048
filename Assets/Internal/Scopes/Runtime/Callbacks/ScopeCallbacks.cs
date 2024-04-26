using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Internal.Scopes.Abstract.Callbacks;

namespace Internal.Scopes.Runtime.Callbacks
{
    public class ScopeCallbacks : IScopeCallbacks, ICallbacksRegistry
    {
        public ScopeCallbacks()
        {
            _callbacks.Add(CallbackStage.Construct, new CallbacksHandler());
            _callbacks.Add(CallbackStage.Dispose, new CallbacksHandler());
        }

        private readonly Dictionary<CallbackStage, ICallbacksStage> _callbacks = new();
        private readonly List<ICallbacksListener> _genericRegisters = new();

        private readonly List<object> _targets = new();

        public void Listen(object listener)
        {
            _targets.Add(listener);
        }

        public void AssignListenersFromTargets()
        {
            foreach (var target in _targets)
            {
                foreach (var (_, handler) in _callbacks)
                    handler.Listen(target);

                foreach (var register in _genericRegisters)
                    register.Listen(target);
            }    
        }

        public void AddScopeCallback<T>(Action<T> invoker, CallbackStage stage, int order)
        {
            var entity = new CallbackEntity<T>(invoker, order);
            _callbacks[stage].Add(entity);
        }

        public void AddScopeAsyncCallback<T>(Func<T, UniTask> invoker, CallbackStage stage, int order)
        {
            var entity = new AsyncCallbackEntity<T>(invoker, order);
            _callbacks[stage].Add(entity);
        }

        public void AddCustomListener(ICallbacksListener callbackRegistry)
        {
            _genericRegisters.Add(callbackRegistry);
        }

        public UniTask RunConstruct()
        {
            return _callbacks[CallbackStage.Construct].Run();
        }
        
        public UniTask RunDispose()
        {
            return _callbacks[CallbackStage.Dispose].Run();
        }
    }
}