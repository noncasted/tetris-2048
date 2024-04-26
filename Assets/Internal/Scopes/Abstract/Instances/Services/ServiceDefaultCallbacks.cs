using Cysharp.Threading.Tasks;
using Internal.Scopes.Abstract.Lifetimes;
using VContainer.Unity;

namespace Internal.Scopes.Abstract.Instances.Services
{
    public interface IScopeAwakeListener
    {
        void OnAwake();
    }

    public interface IScopeAwakeAsyncListener
    {
        UniTask OnAwakeAsync();
    }

    public interface IScopeLifetimeListener
    {
        void OnLifetimeCreated(ILifetime lifetime);
    }

    public interface IScopeEnableListener
    {
        void OnEnabled();
    }

    public interface IScopeEnableAsyncListener
    {
        UniTask OnEnabledAsync();
    }

    public interface IScopeDisableListener
    {
        void OnDisabled();
    }

    public interface IScopeDisableAsyncListener
    {
        UniTask OnDisabledAsync();
    }

    public interface IScopeLoadListener
    {
        void OnLoaded();
    }

    public interface IScopeLoadAsyncListener
    {
        UniTask OnLoadedAsync();
    }

    public interface IScopeBuiltListener
    {
        void OnContainerBuilt(LifetimeScope scope);
    }

    public interface IScopeSwitchListener : IScopeEnableListener, IScopeDisableListener
    {
    }
}