using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Internal.Scopes.Abstract.Callbacks;

namespace Internal.Scopes.Runtime.Callbacks
{
    public class CallbackEntity<T> : ICallbackEntity
    {
        public CallbackEntity(Action<T> invoker, int order)
        {
            _invoker = invoker;
            _order = order;
        }

        private readonly List<T> _targets = new();
        private readonly Action<T> _invoker;

        private readonly int _order;

        public int Order => _order;

        public void Listen(object target)
        {
            if (target is T castedTarget)
                _targets.Add(castedTarget);
        }

        public UniTask InvokeAsync()
        {
            foreach (var target in _targets)
                _invoker?.Invoke(target);

            return UniTask.CompletedTask;
        }
    }


    public class AsyncCallbackEntity<T> : ICallbackEntity
    {
        public AsyncCallbackEntity(Func<T, UniTask> invoker, int order)
        {
            _invoker = invoker;
            _order = order;
        }

        private readonly List<T> _targets = new();
        private readonly Func<T, UniTask> _invoker;

        private readonly int _order;

        public int Order => _order;

        public void Listen(object target)
        {
            if (target is T castedTarget)
                _targets.Add(castedTarget);
        }

        public async UniTask InvokeAsync()
        {
            var count = _targets.Count;
            var internalLoops = new UniTask[count];

            for (var i = 0; i < count; i++)
                internalLoops[i] = _invoker.Invoke(_targets[i]);

            await UniTask.WhenAll(internalLoops);
        }
    }
}