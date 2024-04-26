using Internal.Scopes.Abstract.Options;
using Internal.Scopes.Abstract.Scenes;
using Internal.Services.Options.Implementations;
using Internal.Services.Scenes.Addressable;
using Internal.Services.Scenes.Common;
using Internal.Services.Scenes.Native;
using Internal.Setup.Abstract;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;

namespace Internal.Services.Scenes.Setup
{
    [InlineEditor]
    [CreateAssetMenu(fileName = ScenesFlowRoutes.ServiceName,
        menuName = ScenesFlowRoutes.ServicePath)]
    public class ScenesFlowFactory : ScriptableObject, IInternalServiceFactory
    {
        public void Create(IOptions options, IContainerBuilder services)
        {
            if (options.GetOptions<AssetsOptions>().UseAddressables == true)
            {
                services.Register<AddressablesSceneLoader>(Lifetime.Singleton)
                    .As<ISceneLoader>();

                services.Register<AddressablesScenesUnloader>(Lifetime.Singleton)
                    .As<ISceneUnloader>();
            }
            else
            {
                services.Register<NativeSceneLoader>(Lifetime.Singleton)
                    .As<ISceneLoader>();

                services.Register<NativeSceneUnloader>(Lifetime.Singleton)
                    .As<ISceneUnloader>();
            }
        }
    }
}