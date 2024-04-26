using Internal.Scopes.Abstract.Lifetimes;
using VContainer.Unity;

namespace Internal.Scopes.Abstract.Instances.Services
{
    public interface IServiceScopeData
    {
        LifetimeScope Scope { get; }
        ILifetime Lifetime { get; }
    }
}