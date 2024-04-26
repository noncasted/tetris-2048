using Cysharp.Threading.Tasks;
using Global.System.ApplicationProxies.Abstract;
using Internal.Scopes.Abstract.Containers;
using Internal.Scopes.Abstract.Instances.Services;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Global.System.ApplicationProxies.Runtime
{
    [InlineEditor]
    public class ApplicationProxyFactory : ScriptableObject, IServiceFactory
    {
        public async UniTask Create(IServiceCollection services, IServiceScopeUtils utils)
        {
            services.Register<ApplicationProxy>()
                .As<IScreen>()
                .As<IApplicationFlow>();
        }
    }
}