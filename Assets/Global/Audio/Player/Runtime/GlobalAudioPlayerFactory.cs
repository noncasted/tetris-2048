using Cysharp.Threading.Tasks;
using Global.Audio.Player.Abstract;
using Internal.Scopes.Abstract.Containers;
using Internal.Scopes.Abstract.Instances.Services;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Global.Audio.Player.Runtime
{
    [InlineEditor]
    public class GlobalAudioPlayerFactory : ScriptableObject, IServiceFactory
    {
        [SerializeField] private GlobalAudioPlayer _prefab;

        public UniTask Create(IServiceCollection services, IServiceScopeUtils utils)
        {
            var player = Instantiate(_prefab);
            player.name = "AudioPlayer";

            services.RegisterComponent(player)
                .As<IGlobalVolume>()
                .As<IGlobalAudioPlayer>()
                .AsCallbackListener();

            utils.Binder.MoveToModules(player.gameObject);
            
            return UniTask.CompletedTask;
        }
    }
}