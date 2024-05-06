using Common.DataTypes.Runtime.Attributes;
using Cysharp.Threading.Tasks;
using Features.Loop.Abstract;
using Features.Loop.Runtime.States;
using Internal.Scopes.Abstract.Containers;
using Internal.Scopes.Abstract.Instances.Services;
using UnityEngine;

namespace Features.Loop.Runtime
{
    public class GameLoopFactory : ScriptableObject, IServiceFactory
    {
        [SerializeField] [CreateSO] private GameLoopCheats _cheats;
        
        public UniTask Create(IServiceCollection services, IServiceScopeUtils utils)
        {
            services.Register<GameLoop>()
                .As<IGameLoop>()
                .AsCallbackListener();

            services.Register<GameState>()
                .As<IGameState>();

            services.Register<GamePlayState>();
            services.Register<GameEndState>();

            services.RegisterInstance(_cheats);
            
            return UniTask.CompletedTask;
        }
    }
}