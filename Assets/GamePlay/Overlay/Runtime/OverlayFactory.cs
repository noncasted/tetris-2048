using Common.DataTypes.Runtime.Attributes;
using Cysharp.Threading.Tasks;
using Internal.Scopes.Abstract.Containers;
using Internal.Scopes.Abstract.Instances.Services;
using Internal.Scopes.Abstract.Scenes;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GamePlay.Overlay.Runtime
{
    [InlineEditor]
    public class OverlayFactory : ScriptableObject, IServiceFactory
    {
        [SerializeField] [CreateSO] private SceneData _scene;

        public async UniTask Create(IServiceCollection services, IServiceScopeUtils utils)
        {
            var sceneServices = await utils.LoadTypedOrGetIfMock<OverlaySceneServicesFactory>(_scene);

            sceneServices.Create(services);
        }
    }
}