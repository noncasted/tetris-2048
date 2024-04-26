using Global.System.Updaters.Abstract;
using Internal.Scopes.Abstract.Instances.Services;
using Internal.Scopes.Abstract.Lifetimes;
using UnityEngine;

namespace Global.System.Updaters.Runtime
{
    public class Updater : MonoBehaviour, IUpdater, IUpdateSpeedModifier, IUpdateSpeedSetter, IScopeAwakeListener
    {
        private readonly UpdatablesHandler<IFixedUpdatable> _fixedUpdatables = new();
        private readonly UpdatablesHandler<IPostFixedUpdatable> _postFixedUpdatables = new();
        private readonly UpdatablesHandler<IPreFixedUpdatable> _preFixedUpdatables = new();

        private readonly UpdatablesHandler<IPreUpdatable> _preUpdatables = new();
        private readonly UpdatablesHandler<IUpdatable> _updatables = new();

        private readonly UpdatablesHandler<IUpdateSpeedModifiable> _speedModifiables = new();

        private readonly UpdatablesHandler<IGizmosUpdatable> _gizmosUpdatables = new();

        private bool _isBootstrapped;

        private float _setSpeed = 1f;

        private void Update()
        {
            if (_isBootstrapped == false)
                return;

            var delta = Time.unscaledDeltaTime * Speed;

            _preUpdatables.Fetch();
            _updatables.Fetch();
            _gizmosUpdatables.Fetch();

            foreach (var updatable in _preUpdatables.List)
                updatable.OnPreUpdate(delta);

            foreach (var updatable in _updatables.List)
                updatable.OnUpdate(delta);

            foreach (var updatable in _gizmosUpdatables.List)
                updatable.OnGizmosUpdate();
        }

        private void FixedUpdate()
        {
            if (_isBootstrapped == false)
                return;

            var delta = Time.fixedDeltaTime * Speed;

            _preFixedUpdatables.Fetch();
            _fixedUpdatables.Fetch();
            _postFixedUpdatables.Fetch();

            foreach (var updatable in _preFixedUpdatables.List)
                updatable.OnPreFixedUpdate(delta);

            foreach (var updatable in _fixedUpdatables.List)
                updatable.OnFixedUpdate(delta);

            foreach (var updatable in _postFixedUpdatables.List)
                updatable.OnPostFixedUpdate(delta);
        }

        public void OnAwake()
        {
            _isBootstrapped = true;
        }

        public void Add(IPreUpdatable updatable)
        {
            _preUpdatables.Add(updatable);
        }

        public void Add(IReadOnlyLifetime lifetime, IPreUpdatable updatable)
        {
            _preUpdatables.Add(updatable);
            lifetime.ListenTerminate(() => _preUpdatables.Remove(updatable));
        }

        public void Remove(IPreUpdatable updatable)
        {
            _preUpdatables.Remove(updatable);
        }

        public void Add(IUpdatable updatable)
        {
            _updatables.Add(updatable);
        }

        public void Add(IReadOnlyLifetime lifetime, IUpdatable updatable)
        {
            _updatables.Add(updatable);
            lifetime.ListenTerminate(() => _updatables.Remove(updatable));
        }

        public void Remove(IUpdatable updatable)
        {
            _updatables.Remove(updatable);
        }

        public void Add(IFixedUpdatable updatable)
        {
            _fixedUpdatables.Add(updatable);
        }

        public void Add(IReadOnlyLifetime lifetime, IFixedUpdatable updatable)
        {
            _fixedUpdatables.Add(updatable);
            lifetime.ListenTerminate(() => _fixedUpdatables.Remove(updatable));
        }

        public void Remove(IFixedUpdatable updatable)
        {
            _fixedUpdatables.Remove(updatable);
        }

        public void Add(IPostFixedUpdatable updatable)
        {
            _postFixedUpdatables.Add(updatable);
        }

        public void Add(IReadOnlyLifetime lifetime, IPostFixedUpdatable updatable)
        {
            _postFixedUpdatables.Add(updatable);
            lifetime.ListenTerminate(() => _postFixedUpdatables.Remove(updatable));
        }

        public void Remove(IPostFixedUpdatable updatable)
        {
            _postFixedUpdatables.Remove(updatable);
        }

        public void Add(IGizmosUpdatable updatable)
        {
            _gizmosUpdatables.Add(updatable);
        }

        public void Add(IReadOnlyLifetime lifetime, IGizmosUpdatable updatable)
        {
            _gizmosUpdatables.Add(updatable);
            lifetime.ListenTerminate(() => _gizmosUpdatables.Remove(updatable));
        }

        public void Remove(IGizmosUpdatable updatable)
        {
            _gizmosUpdatables.Add(updatable);
        }

        public void Add(IPreFixedUpdatable updatable)
        {
            _preFixedUpdatables.Add(updatable);
        }

        public void Add(IReadOnlyLifetime lifetime, IPreFixedUpdatable updatable)
        {
            _preFixedUpdatables.Add(updatable);
            lifetime.ListenTerminate(() => _preFixedUpdatables.Remove(updatable));
        }

        public void Remove(IPreFixedUpdatable updatable)
        {
            _preFixedUpdatables.Remove(updatable);
        }

        public float Speed { get; private set; } = 1f;

        public void Add(IUpdateSpeedModifiable modifiable)
        {
            _speedModifiables.Add(modifiable);
        }

        public void Remove(IUpdateSpeedModifiable modifiable)
        {
            _speedModifiables.Remove(modifiable);
        }

        public void Set(float speed)
        {
            if (speed < 0)
                return;

            Speed = speed;

            foreach (var speedModifiable in _speedModifiables.List)
                speedModifiable.OnSpeedModified(speed);
        }

        public void Pause()
        {
            _setSpeed = Speed;

            Set(0f);
        }

        public void Continue()
        {
            Set(_setSpeed);
        }
    }
}