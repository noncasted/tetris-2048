using System;
using Global.System.Updaters.Abstract;
using Internal.Scopes.Abstract.Lifetimes;

namespace Global.System.Updaters.Progressions
{
    public class ProgressionHandle : IProgressionHandle, IUpdatable
    {
        public ProgressionHandle(IUpdater updater)
        {
            _updater = updater;
        }

        private readonly IUpdater _updater;

        private float _targetTime;
        private float _currentTime;
        private Action<float> _callback;
        private IReadOnlyLifetime _lifetime;

        private bool _isSubscribedToUpdate;

        public void Start(IReadOnlyLifetime lifetime, float time, Action<float> callback)
        {
            _callback = callback;
            _targetTime = time;
            _currentTime = 0f;

            if (_isSubscribedToUpdate == false)
            {
                _isSubscribedToUpdate = true;
                _updater.Add(this);
            }

            if (lifetime != _lifetime)
            {
                lifetime?.RemoveTerminationListener(Dispose);
                _lifetime = lifetime;
                _lifetime.ListenTerminate(Dispose);
            }
        }

        public void OnUpdate(float delta)
        {
            _currentTime += delta;

            var progress = _currentTime / _targetTime;
            _callback?.Invoke(progress);
        }

        private void Dispose()
        {
            _updater.Remove(this);
            _isSubscribedToUpdate = false;
        }
    }
}