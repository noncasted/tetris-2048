using System.Collections.Generic;
using Internal.Scopes.Abstract.Callbacks;
using Internal.Scopes.Abstract.Instances.Services;
using Internal.Scopes.Abstract.Lifetimes;
using Internal.Scopes.Abstract.Scenes;
using VContainer.Unity;

namespace Internal.Scopes.Runtime.Instances.Services
{
    public class ScopeLoadResult : IServiceScopeLoadResult
    {
        public ScopeLoadResult(
            LifetimeScope scope,
            ILifetime lifetime,
            IScopeCallbacks callbacks,
            ServiceScopeSceneLoader sceneLoader)
        {
            Scope = scope;
            Lifetime = lifetime;
            Callbacks = callbacks;
            SceneLoader = sceneLoader;
            Scenes = sceneLoader.Results;
        }

        public LifetimeScope Scope { get; }
        public ILifetime Lifetime { get; }
        public IScopeCallbacks Callbacks { get; }
        public ServiceScopeSceneLoader SceneLoader { get; }
        public IReadOnlyList<ISceneLoadResult> Scenes { get; }
    }
}