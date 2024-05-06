using Common.DataTypes.Runtime.Attributes;
using Cysharp.Threading.Tasks;
using Features.Menu.Loop.Abstract;
using Internal.Scopes.Abstract.Containers;
using Internal.Scopes.Abstract.Instances.Services;
using Internal.Scopes.Abstract.Scenes;
using UnityEngine;

namespace Features.Menu.Loop.Runtime
{
    public class MenuLoopFactory : ScriptableObject, IServiceFactory
    {
        [SerializeField] [CreateSO] private SceneData _scene;
        
        public async UniTask Create(IServiceCollection services, IServiceScopeUtils utils)
        {
            var sceneServices = await utils.LoadTypedOrGetIfMock<MenuSceneServicesFactory>(_scene);
            sceneServices.Create(services);
            
            services.Register<MenuMain>()
                .As<IMenuMain>()
                .AsCallbackListener();
        }
    }
}