using Cysharp.Threading.Tasks;
using Global.Audio.Listener.Abstract;
using Internal.Scopes.Abstract.Containers;
using Internal.Scopes.Abstract.Instances.Services;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Global.Audio.Listener.Runtime
{
    [InlineEditor]
    public class GlobalAudioListenerFactory : ScriptableObject, IServiceFactory
    {
        [SerializeField] private GlobalGlobalAudioListener _prefab;

        public async UniTask Create(IServiceCollection services, IServiceScopeUtils utils)
        {
            var switcher = Instantiate(_prefab);
            switcher.name = "AudioListener";
            utils.Binder.MoveToModules(switcher.gameObject);

            services.RegisterComponent(switcher)
                .As<IGlobalAudioListener>();
        }
    }
}