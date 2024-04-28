using Common.DataTypes.Runtime.Attributes;
using Cysharp.Threading.Tasks;
using Internal.Scopes.Abstract.Containers;
using Internal.Scopes.Abstract.Instances.Services;
using Internal.Scopes.Abstract.Scenes;
using Tutorial.Runtime.Steps.CombineWithFall;
using Tutorial.Runtime.Steps.EndConditions;
using Tutorial.Runtime.Steps.MoveAndCombine;
using Tutorial.Runtime.Steps.SpeedModifications;
using UnityEngine;

namespace Tutorial.Runtime
{
    public class TutorialFactory : ScriptableObject, IServiceFactory
    {
        [SerializeField] [CreateSO] private SceneData _data;

        public async UniTask Create(IServiceCollection services, IServiceScopeUtils utils)
        {
            var servicesFactory = await utils.LoadTypedOrGetIfMock<TutorialSceneServicesFactory>(_data);
            servicesFactory.Create(services);

            services.Register<TutorialStep_MoveAndCombine>();
            services.Register<TutorialStep_CombineWithFall>();
            services.Register<TutorialStep_SpeedModifications>();
            services.Register<TutorialStep_EndConditions>();
            services.Register<TutorialState>();
        }
    }
}