using Cysharp.Threading.Tasks;
using Internal.Scopes.Abstract.Containers;
using Internal.Scopes.Abstract.Instances.Services;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Internal.Scopes.Common.Services
{
    [InlineEditor]
    [CreateAssetMenu(fileName = "DefaultCallbacks", menuName = ScopesRoutes.Root + "DefaultCallbacks")]
    public class ServiceDefaultCallbacksFactory : ScriptableObject, IServiceFactory
    {
        public async UniTask Create(IServiceCollection services, IServiceScopeUtils utils)
        {
            var callbacks = new ServiceDefaultCallbacksRegister();
            callbacks.AddCallbacks(utils.Callbacks, utils.Data);
        }
    }
}