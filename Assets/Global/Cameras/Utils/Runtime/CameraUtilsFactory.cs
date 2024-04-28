using Cysharp.Threading.Tasks;
using Global.Cameras.Utils.Abstract;
using Internal.Scopes.Abstract.Containers;
using Internal.Scopes.Abstract.Instances.Services;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Global.Cameras.Utils.Runtime
{
    [InlineEditor]
    public class CameraUtilsFactory : ScriptableObject, IServiceFactory
    {
        public UniTask Create(IServiceCollection services, IServiceScopeUtils utils)
        {
            services.Register<CameraUtils>()
                .As<ICameraUtils>()
                .AsCallbackListener();
            
            return UniTask.CompletedTask;
        }
    }
}