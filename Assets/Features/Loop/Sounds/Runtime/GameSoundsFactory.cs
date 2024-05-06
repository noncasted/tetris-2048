using Common.DataTypes.Runtime.Attributes;
using Cysharp.Threading.Tasks;
using Features.Loop.Sounds.Abstract;
using Internal.Scopes.Abstract.Containers;
using Internal.Scopes.Abstract.Instances.Services;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Features.Loop.Sounds.Runtime
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