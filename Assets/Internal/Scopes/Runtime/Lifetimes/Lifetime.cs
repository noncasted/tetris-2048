using System;
using System.Collections.Generic;
using System.Threading;
using Internal.Scopes.Abstract.Lifetimes;
using UnityEngine;

namespace Internal.Scopes.Runtime.Lifetimes
{
    public class Lifetime : ILifetime
    {
        private readonly List<Lifetime> _children = new();
        private readonly List<Action> _terminationListeners = new();
        private readonly CancellationTokenSource _cancellation = new();

        private bool _isTerminated;

        public CancellationToken Token => _cancellation.Token;
        public bool IsTerminated => _isTerminated;

        public void ListenTerminate(Action callback)
        {
            if (_isTerminated == true)
            {
                Debug.LogError("Trying to listen terminated lifetime");
                callback?.Invoke();
                return;
            }

            _terminationListeners.Add(callback);
        }

        public void RemoveTerminationListener(Action callback)
        {
            _terminationListeners.Remove(callback);
        }

        public ILifetime CreateChild()
        {
            if (_isTerminated == true)
            {
                Debug.LogError("Trying to create child from terminated lifetime");

                return new TerminatedLifetime();
            }

            var lifetime = new Lifetime();
            _children.Add(lifetime);

            return lifetime;
        }

        public void Terminate()
        {
            if (_isTerminated == true)
                return;

            _isTerminated = true;

            foreach (var child in _children)
                child.Terminate();

            _cancellation.Cancel();

            foreach (var listener in _terminationListeners)
                listener.Invoke();
        }
    }
}