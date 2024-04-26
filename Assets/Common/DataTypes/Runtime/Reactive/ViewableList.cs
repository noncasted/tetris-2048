using System;
using System.Collections.Generic;
using Internal.Scopes.Abstract.Lifetimes;
using Internal.Scopes.Runtime.Lifetimes;

namespace Common.DataTypes.Runtime.Reactive
{
    public class ViewableList<T> : IViewableList<T>
    {
        private readonly List<Action<IReadOnlyLifetime, T>> _listeners = new();

        private readonly List<IReadOnlyLifetime> _lifetimes = new();
        private readonly Dictionary<T, ValueHandler> _handlers = new();

        public readonly List<T> Values = new();

        public void View(IReadOnlyLifetime lifetime, Action<IReadOnlyLifetime, T> listener)
        {
            _listeners.Add(listener);

            lifetime.ListenTerminate(RemoveListener);

            return;

            void RemoveListener()
            {
                _listeners.Remove(listener);
            }
        }

        public void Add(T value)
        {
            var lifetime = new Lifetime();
            var handler = new ValueHandler(lifetime);

            _handlers.Add(value, handler);

            foreach (var listener in _listeners)
                listener?.Invoke(lifetime, value);
        }

        public void Remove(T value)
        {
            _handlers[value].Lifetime.Terminate();
            _handlers.Remove(value);
        }

        public void Clear()
        {
            var remove = new List<T>();

            foreach (var (value, handler) in _handlers)
            {
                handler.Lifetime.Terminate();
                remove.Add(value);
            }

            while (remove.Count != 0)
            {
                _handlers.Remove(remove[0]);
                remove.RemoveAt(0);
            }
        }

        public void Fire()
        {
            foreach (var (_, handler) in _handlers)
                handler.OnValidation();

            foreach (var value in Values)
            {
                if (_handlers.TryGetValue(value, out var handler) == false)
                {
                    var lifetime = new Lifetime();
                    handler = new ValueHandler(lifetime);

                    _handlers.Add(value, handler);

                    foreach (var listener in _listeners)
                        listener?.Invoke(lifetime, value);
                }
                else
                {
                    handler.SetValid();
                }
            }

            var remove = new List<T>();

            foreach (var (value, handler) in _handlers)
            {
                if (handler.IsValid == true)
                    continue;

                handler.Lifetime.Terminate();
                remove.Add(value);
            }

            while (remove.Count != 0)
            {
                _handlers.Remove(remove[0]);
                remove.RemoveAt(0);
            }
        }

        private class ValueHandler
        {
            public ValueHandler(Lifetime lifetime)
            {
                Lifetime = lifetime;
                _isValid = true;
            }

            public readonly Lifetime Lifetime;

            private bool _isValid;

            public bool IsValid => _isValid;

            public void OnValidation()
            {
                _isValid = false;
            }

            public void SetValid()
            {
                _isValid = true;
            }
        }
    }
}