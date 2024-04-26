using Internal.Scopes.Abstract.Callbacks;
using Internal.Scopes.Abstract.Lifetimes;
using Internal.Scopes.Abstract.Options;
using VContainer.Unity;

namespace Internal.Scopes.Abstract.Instances.Entities
{
    public interface IScopedEntityUtils
    {
        IOptions Options { get; }
        LifetimeScope Scope { get; }
        ILifetime Lifetime { get; }
        ICallbacksRegistry Callbacks { get; }
    }
}