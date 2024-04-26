using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Internal.Scopes.Abstract.Containers;
using Internal.Scopes.Abstract.Instances.Services;
using Internal.Scopes.Abstract.Options;
using Internal.Scopes.Abstract.Scenes;
using Internal.Scopes.Runtime.Callbacks;
using Internal.Scopes.Runtime.Containers;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Lifetime = Internal.Scopes.Runtime.Lifetimes.Lifetime;

namespace Internal.Scopes.Runtime.Instances.Services
{
    public class ServiceScopeLoader : IServiceScopeLoader
    {
        public ServiceScopeLoader(ISceneLoader sceneLoader, IOptions options)
        {
            _sceneLoader = sceneLoader;
            _options = options;
        }

        private readonly ISceneLoader _sceneLoader;
        private readonly IOptions _options;

        public async UniTask<IServiceScopeLoadResult> Load(LifetimeScope parent, IServiceScopeConfig config)
        {
            var sceneLoader = new ServiceScopeSceneLoader( _sceneLoader);
            var utils = await CreateUtils(sceneLoader, config);
            var builder = new ServiceCollection();

            await CreateServices(builder, utils, config);
            await BuildContainer(builder, utils, parent);

            utils.InternalCallbacks.AssignListenersFromTargets();
            
            var loadResult = new ScopeLoadResult(
                utils.Data.Scope,
                utils.Data.Lifetime,
                utils.InternalCallbacks,
                sceneLoader);

            return loadResult;
        }

        private async UniTask<ServiceScopeUtils> CreateUtils(
            ISceneLoader sceneLoader,
            IServiceScopeConfig config)
        {
            var servicesScene = await sceneLoader.Load(config.ServicesScene);
            var binder = new ServiceScopeBinder(servicesScene.Scene);
            var scope = Object.Instantiate(config.ScopePrefab);
            binder.MoveToModules(scope.gameObject);
            var lifetime = new Lifetime();
            var scopeData = new ServiceScopeData(scope, lifetime);
            var callbacks = new ScopeCallbacks();

            var utils = new ServiceScopeUtils(_options, sceneLoader, binder, scopeData, callbacks, config.IsMock);

            return utils;
        }

        private async UniTask CreateServices(
            IServiceCollection builder,
            IServiceScopeUtils utils,
            IServiceScopeConfig config)
        {
            var tasks = new List<UniTask>();

            var services = new List<IServiceFactory>(config.Services);

            foreach (var compose in config.Composes)
                services.AddRange(compose.Factories);

            foreach (var factory in services)
                tasks.Add(factory.Create(builder, utils));

            await UniTask.WhenAll(tasks);
        }

        private async UniTask BuildContainer(
            ServiceCollection builder,
            IServiceScopeUtils utils,
            LifetimeScope parent)
        {
            using (LifetimeScope.EnqueueParent(parent))
            {
                using (LifetimeScope.Enqueue(Register))
                {
                    await UniTask.Create(async () => utils.Data.Scope.Build());
                }
            }

            builder.Resolve(utils.Data.Scope.Container, utils.Callbacks);
            return;

            void Register(IContainerBuilder container)
            {
                builder.PassRegistrations(container);
            }
        }
    }
}