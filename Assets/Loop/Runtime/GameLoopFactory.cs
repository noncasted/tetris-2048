using Cysharp.Threading.Tasks;
using Internal.Scopes.Abstract.Containers;
using Internal.Scopes.Abstract.Instances.Services;
using Loop.Abstract;
using Loop.Runtime.States;
using UnityEngine;

namespace Loop.Runtime
{
    public class GameLoopFactory : ScriptableObject, IServiceFactory
    {
        public async UniTask Create(IServiceCollection services, IServiceScopeUtils utils)
        {
            services.Register<GameLoop>()
                .As<IGameLoop>()
                .AsCallbackListener();

            services.Register<GameState>()
                .As<IGameState>();

            services.Register<GamePlayState>();
            services.Register<GameEndState>();
        }
    }
}