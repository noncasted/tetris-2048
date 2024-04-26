using Common.Tools.SceneServices;
using GamePlay.Boards.Abstract.Boards;
using GamePlay.Boards.Runtime.Boards;
using Internal.Scopes.Abstract.Containers;
using UnityEngine;

namespace GamePlay.Loop.Runtime
{
    [DisallowMultipleComponent]
    public class GamePlaySceneServicesFactory : SceneServicesFactory
    {
        [SerializeField] private BoardView _board;
        [SerializeField] private GamePlayRoot _root;

        protected override void CollectServices(IServiceCollection services)
        {
            services.RegisterComponent(_board)
                .As<IBoardView>()
                .As<IBoardLifecycle>();
            
            services.Inject(_root);
        }
    }
}