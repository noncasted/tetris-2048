using System;
using Common.DataTypes.Runtime.Collections;
using Internal.Scopes.Abstract.Lifetimes;

namespace Common.DataTypes.Runtime.Reactive
{
    public class ViewableDelegate : IViewableDelegate
    {
        private readonly ModifiableList<Action> _listeners = new();

        public void Invoke()
        {
            foreach (var listener in _listeners)
                listener?.Invoke();
        }

        public void Listen(IReadOnlyLifetime lifetime, Action listener)
        {
            _listeners.Add(listener);

            lifetime.ListenTerminate(() => _listeners.Remove(listener));
        }
    }

    public class ViewableDelegate<T> : IViewableDelegate<T>
    {
        private readonly ModifiableList<Action<T>> _listeners = new();

        public void Invoke(T value)
        {
            foreach (var listener in _listeners)
                listener?.Invoke(value);
        }

        public void Listen(IReadOnlyLifetime lifetime, Action<T> listener)
        {
            _listeners.Add(listener);

            lifetime.ListenTerminate(() => _listeners.Remove(listener));
        }
    }

    public class ViewableDelegate<T1, T2> : IViewableDelegate<T1, T2>
    {
        private readonly ModifiableList<Action<T1, T2>> _listeners = new();

        public void Invoke(T1 value1, T2 value2)
        {
            foreach (var listener in _listeners)
                listener?.Invoke(value1, value2);
        }

        public void Listen(IReadOnlyLifetime lifetime, Action<T1, T2> listener)
        {
            _listeners.Add(listener);

            lifetime.ListenTerminate(() => _listeners.Remove(listener));
        }
    }
}