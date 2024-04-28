using Cysharp.Threading.Tasks;
using Global.Cameras.CurrentProvider.Abstract;
using Internal.Scopes.Abstract.Containers;
using Internal.Scopes.Abstract.Instances.Services;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Global.Cameras.CurrentProvider.Runtime
{
    [InlineEditor]
    public class CurrentCameraProviderFactory : ScriptableObject, IServiceFactory
    {
        public UniTask Create(IServiceCollection services, IServiceScopeUtils utils)
        {
            services.Register<CurrentCameraProvider>()
                .As<ICurrentCameraProvider>()
                .AsCallbackListener();
            
            return UniTask.CompletedTask;
        }
    }
}