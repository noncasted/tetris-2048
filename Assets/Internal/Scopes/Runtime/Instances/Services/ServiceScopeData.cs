using Internal.Scopes.Abstract.Instances.Services;
using Internal.Scopes.Abstract.Lifetimes;
using VContainer.Unity;

namespace Internal.Scopes.Runtime.Instances.Services
{
    public class ServiceScopeData : IServiceScopeData
    {
        public ServiceScopeData(LifetimeScope scope, ILifetime lifetime)
        {
            _scope = scope;
            _lifetime = lifetime;
        }

        private readonly LifetimeScope _scope;
        private readonly ILifetime _lifetime;

        public LifetimeScope Scope => _scope;
        public ILifetime Lifetime => _lifetime;
    }
}