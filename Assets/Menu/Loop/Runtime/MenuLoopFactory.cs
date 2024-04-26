using Common.DataTypes.Runtime.Attributes;
using Cysharp.Threading.Tasks;
using Internal.Scopes.Abstract.Containers;
using Internal.Scopes.Abstract.Instances.Services;
using Internal.Scopes.Abstract.Scenes;
using Menu.Loop.Abstract;
using UnityEngine;

namespace Menu.Loop.Runtime
{
    public class MenuLoopFactory : ScriptableObject, IServiceFactory
    {
        [SerializeField] [CreateSO] private SceneData _scene;
        
        public async UniTask Create(IServiceCollection services, IServiceScopeUtils utils)
        {
            var sceneServices = await utils.LoadTypedOrGetIfMock<MenuSceneServicesFactory>(_scene);
            sceneServices.Create(services);
            
            services.Register<MenuLoop>()
                .As<IMenuLoop>()
                .AsCallbackListener();
        }
    }
}