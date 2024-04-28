using Cysharp.Threading.Tasks;
using Global.Configs.Upgrades.Abstract;
using Internal.Scopes.Abstract.Containers;
using Internal.Scopes.Abstract.Instances.Services;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Global.Configs.Upgrades.Runtime
{
    [InlineEditor]
    public class UpgradesFactory : ScriptableObject, IServiceFactory
    {
        public UniTask Create(IServiceCollection services, IServiceScopeUtils utils)
        {
            services.Register<Upgrades>()
                .As<IUpgrades>()
                .AsCallbackListener();
            
            return UniTask.CompletedTask;
        }
    }
}