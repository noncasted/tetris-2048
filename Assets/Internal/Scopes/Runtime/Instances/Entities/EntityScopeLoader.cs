using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Internal.Scopes.Abstract.Containers;
using Internal.Scopes.Abstract.Instances.Entities;
using Internal.Scopes.Abstract.Options;
using Internal.Scopes.Runtime.Callbacks;
using Internal.Scopes.Runtime.Containers;
using VContainer;
using VContainer.Unity;
using Lifetime = Internal.Scopes.Runtime.Lifetimes.Lifetime;

namespace Internal.Scopes.Runtime.Instances.Entities
{
    public class EntityScopeLoader : IEntityScopeLoader
    {
        public EntityScopeLoader(IOptions options)
        {
            _options = options;
        }

        private readonly IOptions _options;

        public UniTask<T> Load<T>(
            LifetimeScope parent,
            IScopedEntityViewFactory viewFactory,
            IScopedEntityConfig config)
        {
            return Load<T>(parent, viewFactory, config, Array.Empty<IComponentFactory>());
        }

        public async UniTask<T> Load<T>(
            LifetimeScope parent,
            IScopedEntityViewFactory viewFactory,
            IScopedEntityConfig config,
            IReadOnlyList<IComponentFactory> runtimeFactories)
        {
            var utils = CreateUtils(viewFactory);
            var builder = new ServiceCollection();

            CreateServices(builder, utils, config, viewFactory, runtimeFactories);
            await BuildContainer(builder, utils, parent);

            utils.InternalCallbacks.AssignListenersFromTargets();
            
            return viewFactory.Scope.Container.Resolve<T>();
        }

        private ScopedEntityUtils CreateUtils(IScopedEntityViewFactory viewFactory)
        {
            var callbacks = new ScopeCallbacks();
            var lifetime = new Lifetime();

            var utils = new ScopedEntityUtils(_options, viewFactory.Scope, lifetime, callbacks);

            return utils;
        }

        private void CreateServices(
            IServiceCollection services,
            IScopedEntityUtils utils,
            IScopedEntityConfig config,
            IScopedEntityViewFactory viewFactory,
            IReadOnlyList<IComponentFactory> runtimeFactories)
        {
            foreach (var compose in config.Composes)
            {
                foreach (var factory in compose.Factories)
                    factory.Create(services, utils);
            }

            foreach (var factory in config.Components)
                factory.Create(services, utils);

            foreach (var factory in runtimeFactories)
                factory.Create(services, utils);

            viewFactory.CreateViews(services, utils);
        }

        private async UniTask BuildContainer(
            ServiceCollection builder,
            IScopedEntityUtils utils,
            LifetimeScope parent)
        {
            using (LifetimeScope.EnqueueParent(parent))
            {
                using (LifetimeScope.Enqueue(Register))
                {
                    await UniTask.Create(async () => utils.Scope.Build());
                }
            }

            builder.Resolve(utils.Scope.Container, utils.Callbacks);
            
            return;

            void Register(IContainerBuilder container)
            {
                builder.PassRegistrations(container);
            }
        }
    }
}