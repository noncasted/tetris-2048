using Cysharp.Threading.Tasks;
using Internal.Scopes.Abstract.Containers;
using Internal.Scopes.Abstract.Instances.Services;
using Loop.Setup;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Global.GameLoops.Runtime
{
    [InlineEditor]
    public class GlobalLoopFactory : ScriptableObject, IServiceFactory
    {
        [SerializeField] private GameScopeConfig _gameScope;

        public virtual UniTask Create(IServiceCollection services, IServiceScopeUtils utils)
        {
            if (utils.IsMock == true)            
                return UniTask.CompletedTask;;
            
            services.Register<GlobalLoop>()
                .WithParameter(_gameScope)
                .WithParameter(utils.Data.Scope)
                .AsSelfResolvable()
                .AsCallbackListener();
            
            return UniTask.CompletedTask;
        }
    }
}