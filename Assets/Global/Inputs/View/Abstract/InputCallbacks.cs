using System.Collections.Generic;
using Internal.Scopes.Abstract.Callbacks;
using Internal.Scopes.Abstract.Lifetimes;

namespace Global.Inputs.View.Abstract
{
    public interface IInputConstructListener
    {
        void OnInputConstructed(IReadOnlyLifetime lifetime);
    }

    public class InputCallbacks : ICallbacksListener
    {
        private readonly List<IInputConstructListener> _listeners = new();

        public void Listen(object target)
        {
            if (target is IInputConstructListener listener)
                _listeners.Add(listener);
        }

        public void Invoke(IReadOnlyLifetime lifetime)
        {
            foreach (var listener in _listeners)
                listener.OnInputConstructed(lifetime);
        }
    }
}