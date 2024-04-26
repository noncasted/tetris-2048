using System;
using System.Collections.Generic;
using Internal.Scopes.Abstract.Lifetimes;
using Internal.Scopes.Runtime.Lifetimes;

namespace Common.DataTypes.Runtime.Reactive
{
    public class ViewableProperty<T> : IViewableProperty<T>
    {
        public ViewableProperty()
        {
            _value = default;
            _currentValueLifetime = new Lifetime();
        }

        public ViewableProperty(T value)
        {
            _value = value;
            _currentValueLifetime = new Lifetime();
        }

        private readonly List<Action<IReadOnlyLifetime, T>> _listeners = new();

        private ILifetime _currentValueLifetime;
        private T _value;

        public T Value => _value;

        public void View(IReadOnlyLifetime lifetime, Action<IReadOnlyLifetime, T> listener)
        {
            _listeners.Add(listener);

            listener.Invoke(_currentValueLifetime, _value);
            lifetime.ListenTerminate(RemoveListener);

            return;

            void RemoveListener()
            {
                _listeners.Remove(listener);
            }
        }

        public void Set(T value)
        {
            _value = value;
            _currentValueLifetime.Terminate();
            _currentValueLifetime = new Lifetime();

            foreach (var listener in _listeners)
                listener?.Invoke(_currentValueLifetime, value);
        }
    }
}