using Common.DataTypes.Runtime.Attributes;
using Cysharp.Threading.Tasks;
using Internal.Scopes.Abstract.Containers;
using Internal.Scopes.Abstract.Instances.Services;
using Loop.Sounds.Abstract;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Loop.Sounds.Runtime
{
    [InlineEditor]
    public class GameSoundsFactory : ScriptableObject, IServiceFactory
    {
        [SerializeField] [CreateSO] private GameSoundsConfig _config;

        public UniTask Create(IServiceCollection services, IServiceScopeUtils utils)
        {
            services.Register<GameSounds>()
                .WithParameter(_config)
                .As<IGameSounds>();
            
            return UniTask.CompletedTask;
        }
    }
}