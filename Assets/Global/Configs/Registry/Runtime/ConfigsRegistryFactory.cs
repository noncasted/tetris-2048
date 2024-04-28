using Common.DataTypes.Runtime.Attributes;
using Cysharp.Threading.Tasks;
using Internal.Scopes.Abstract.Containers;
using Internal.Scopes.Abstract.Instances.Services;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Global.Configs.Registry.Runtime
{
    [InlineEditor]
    public class ConfigsRegistryFactory : ScriptableObject, IServiceFactory
    {
        [SerializeField] [CreateSO] private ConfigsRegistry _registry;

        public UniTask Create(IServiceCollection services, IServiceScopeUtils utils)
        {
            foreach (var source in _registry.Objects)
                source.CreateInstance(services);

            services.Register<Configs>()
                .As<IConfigs>()
                .AsCallbackListener();
            
            return UniTask.CompletedTask;
        }
    }
}