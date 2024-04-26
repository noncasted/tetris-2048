using Internal.Scopes.Abstract.Callbacks;
using Internal.Scopes.Abstract.Instances.Entities;
using Internal.Scopes.Abstract.Lifetimes;
using Internal.Scopes.Abstract.Options;
using Internal.Scopes.Runtime.Callbacks;
using VContainer.Unity;

namespace Internal.Scopes.Runtime.Instances.Entities
{
    public class ScopedEntityUtils : IScopedEntityUtils
    {
        public ScopedEntityUtils(
            IOptions options,
            LifetimeScope scope,
            ILifetime lifetime,
            ScopeCallbacks callbacks)
        {
            Options = options;
            Scope = scope;
            Lifetime = lifetime;
            InternalCallbacks = callbacks;
        }

        public IOptions Options { get; }

        public LifetimeScope Scope { get; }

        public ILifetime Lifetime { get; }
        public ScopeCallbacks InternalCallbacks { get; }

        public ICallbacksRegistry Callbacks => InternalCallbacks;
    }
}