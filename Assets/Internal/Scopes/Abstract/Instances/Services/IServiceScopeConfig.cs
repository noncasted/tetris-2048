using System.Collections.Generic;
using Internal.Scopes.Abstract.Scenes;
using VContainer.Unity;

namespace Internal.Scopes.Abstract.Instances.Services
{
    public interface IServiceScopeConfig
    {
        LifetimeScope ScopePrefab { get; }
        SceneData ServicesScene { get; }
        bool IsMock { get; }

        IReadOnlyList<IServiceFactory> Services { get; }
        IReadOnlyList<IServicesCompose> Composes { get; }
    }
}