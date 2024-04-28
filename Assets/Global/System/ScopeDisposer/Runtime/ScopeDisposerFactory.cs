using Cysharp.Threading.Tasks;
using Global.System.ScopeDisposer.Abstract;
using Internal.Scopes.Abstract.Containers;
using Internal.Scopes.Abstract.Instances.Services;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Global.System.ScopeDisposer.Runtime
{
    [InlineEditor]
    public class ScopeDisposerFactory : ScriptableObject, IServiceFactory
    {
        public UniTask Create(IServiceCollection services, IServiceScopeUtils utils)
        {
            services.Register<ScopeDisposer>()
                .As<IScopeDisposer>();
            
            return UniTask.CompletedTask;
        }
    }
}