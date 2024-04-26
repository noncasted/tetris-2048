using Cysharp.Threading.Tasks;
using Internal.Scopes.Abstract.Instances.Entities;
using Internal.Scopes.Abstract.Instances.Services;
using Internal.Scopes.Abstract.Options;
using Internal.Scopes.Runtime.Instances.Entities;
using Internal.Scopes.Runtime.Instances.Services;
using Internal.Setup.Abstract;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Internal.Setup.Runtime
{
    public class InternalScopeLoader
    {
        public InternalScopeLoader(IInternalScopeConfig config)
        {
            _config = config;
        }

        private readonly IInternalScopeConfig _config;

        public async UniTask<LifetimeScope> Load()
        {
            _config.OptionsSetup.Setup();
            
            var scope = Object.Instantiate(_config.Scope);

            using (LifetimeScope.Enqueue(Register))
            {
                await UniTask.Create(async () => scope.Build());
            }

            void Register(IContainerBuilder services)
            {
                foreach (var service in _config.Services)
                    service.Create(_config.Options, services);

                services.RegisterInstance(_config.Options)
                    .As<IOptions>();
                
                services.Register<ServiceScopeLoader>(Lifetime.Singleton)
                    .As<IServiceScopeLoader>();
                
                services.Register<EntityScopeLoader>(Lifetime.Singleton)
                    .As<IEntityScopeLoader>();

            }

            return scope;
        }
    }
}