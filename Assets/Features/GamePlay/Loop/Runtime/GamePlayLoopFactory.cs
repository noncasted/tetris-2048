﻿using Common.DataTypes.Runtime.Attributes;
using Common.Tools.SceneServices;
using Cysharp.Threading.Tasks;
using Features.GamePlay.BlockSpawners.Abstract;
using Features.GamePlay.BlockSpawners.Runtime;
using Features.GamePlay.Boards.Abstract.Blocks;
using Features.GamePlay.Boards.Abstract.Boards;
using Features.GamePlay.Boards.Abstract.Factory;
using Features.GamePlay.Boards.Abstract.Moves;
using Features.GamePlay.Boards.Runtime.Boards;
using Features.GamePlay.Boards.Runtime.Factory;
using Features.GamePlay.Boards.Runtime.Moves;
using Features.GamePlay.Input.Abstract;
using Features.GamePlay.Input.Runtime;
using Features.GamePlay.Loop.Abstract;
using Features.GamePlay.Loop.Scores.Abstract;
using Features.GamePlay.Loop.Scores.Runtime;
using Features.GamePlay.Save.Abstract;
using Features.GamePlay.Save.Runtime;
using Features.Loop.Abstract;
using Internal.Scopes.Abstract.Containers;
using Internal.Scopes.Abstract.Instances.Services;
using Internal.Scopes.Abstract.Scenes;
using UnityEngine;

namespace Features.GamePlay.Loop.Runtime
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