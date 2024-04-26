using System.Collections.Generic;
using Internal.Scopes.Abstract.Callbacks;
using Internal.Scopes.Abstract.Lifetimes;
using Internal.Scopes.Abstract.Scenes;
using VContainer.Unity;

namespace Internal.Scopes.Abstract.Instances.Services
{
    public interface IServiceScopeLoadResult
    {
        LifetimeScope Scope { get; }
        ILifetime Lifetime { get; }
        IScopeCallbacks Callbacks { get; }
        IReadOnlyList<ISceneLoadResult> Scenes { get; }
    }
}