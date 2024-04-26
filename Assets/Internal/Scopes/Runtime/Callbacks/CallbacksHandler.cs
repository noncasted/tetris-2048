using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Internal.Scopes.Abstract.Callbacks;

namespace Internal.Scopes.Runtime.Callbacks
{
    public class CallbacksHandler : ICallbacksStage
    {
        private readonly List<ICallbackEntity> _callbacks = new();

        public void Add(ICallbackEntity handler)
        {
            _callbacks.Add(handler);
        }

        public void Listen(object listener)
        {
            foreach (var callbackHandler in _callbacks)
                callbackHandler.Listen(listener);
        }

        public async UniTask Run()
        {
            var orderedCallbacks = _callbacks.OrderBy(t => t.Order);

            foreach (var handler in orderedCallbacks)
                await handler.InvokeAsync();
        }
    }
}