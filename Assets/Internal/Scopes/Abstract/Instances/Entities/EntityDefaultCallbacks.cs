using Cysharp.Threading.Tasks;
using Internal.Scopes.Abstract.Lifetimes;

namespace Internal.Scopes.Abstract.Instances.Entities
{
    public interface IEntityAwakeListener
    {
        void OnAwake();
    }

    public interface IEntityAwakeAsyncListener
    {
        UniTask OnAwakeAsync();
    }

    public interface IEntityLifetimeListener
    {
        void OnLifetimeCreated(ILifetime lifetime);
    }

    public interface IEntitySwitchLifetimeListener
    {
        public void OnSwitchLifetimeCreated(ILifetime lifetime);
    }

    public interface IEntityEnableListener
    {
        void OnEnabled();
    }

    public interface IEntityEnableAsyncListener
    {
        UniTask OnEnabledAsync();
    }

    public interface IEntityDisableListener
    {
        void OnDisabled();
    }

    public interface IEntityDisableAsyncListener
    {
        UniTask OnDisabledAsync();
    }

    public interface IEntityDestroyListener
    {
        void OnDestroyed();
    }

    public interface IEntityDestroyAsyncListener
    {
        UniTask OnDestroyedAsync();
    }

    public interface IEntitySwitchListener : IEntityEnableListener, IEntityDisableListener
    {
    }
}