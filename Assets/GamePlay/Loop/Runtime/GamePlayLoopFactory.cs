using Common.DataTypes.Runtime.Attributes;
using Common.Tools.SceneServices;
using Cysharp.Threading.Tasks;
using GamePlay.BlockSpawners.Abstract;
using GamePlay.BlockSpawners.Runtime;
using GamePlay.Boards.Abstract.Blocks;
using GamePlay.Boards.Abstract.Boards;
using GamePlay.Boards.Abstract.Factory;
using GamePlay.Boards.Abstract.Moves;
using GamePlay.Boards.Runtime.Boards;
using GamePlay.Boards.Runtime.Factory;
using GamePlay.Boards.Runtime.Moves;
using GamePlay.Input.Abstract;
using GamePlay.Input.Runtime;
using GamePlay.Loop.Abstract;
using GamePlay.Loop.Scores.Abstract;
using GamePlay.Loop.Scores.Runtime;
using GamePlay.Save.Abstract;
using GamePlay.Save.Runtime;
using Internal.Scopes.Abstract.Containers;
using Internal.Scopes.Abstract.Instances.Services;
using Internal.Scopes.Abstract.Scenes;
using Loop.Abstract;
using UnityEngine;

namespace GamePlay.Loop.Runtime
{
    public class GamePlayLoopFactory : ScriptableObject, IServiceFactory
    {
        [SerializeField] [CreateSO] private SceneData _scene;
        [SerializeField] [CreateSO] private BlockFactoryConfig _blockFactoryConfig;
        [SerializeField] [CreateSO] private BlockSpawnerConfig _blockSpawnerConfig;
        [SerializeField] [CreateSO] private BlockColors _blockColors;
        [SerializeField] [CreateSO] private GameSpeedConfig _speedConfig;
        
        public async UniTask Create(IServiceCollection services, IServiceScopeUtils utils)
        {
            SceneServicesFactory sceneServices;
            
            if (utils.IsMock == false)
            {
                var loadedSceneServices = await utils.SceneLoader.LoadTyped<GamePlaySceneServicesFactory>(_scene);
                sceneServices = loadedSceneServices;
            }
            else
            {
                sceneServices = FindFirstObjectByType<GamePlaySceneServicesFactory>();
            }

            sceneServices.Create(services);

            services.Register<GamePlayLoop>()
                .As<IGamePlayLoop>()
                .AsCallbackListener();

            services.Register<Board>()
                .As<IBoard>();

            services.Register<GameInput>()
                .As<IGameInput>()
                .AsCallbackListener();

            services.Register<BlockFactory>()
                .WithParameter(_blockFactoryConfig)
                .As<IBlockFactory>();

            services.Register<BlockSpawner>()
                .WithParameter(_blockSpawnerConfig)
                .As<IBlockSpawner>()
                .AsCallbackListener();

            services.Register<BoardScanner>()
                .As<IBoardScanner>();

            services.Register<BoardMover>()
                .As<IBoardMover>();

            services.Register<Score>()
                .As<IScore>()
                .AsCallbackListener();
            
            services.Register<GameSaver>()
                .As<IGameSaver>()
                .AsCallbackListener();

            services.RegisterInstance(_speedConfig);

        }
    }
}