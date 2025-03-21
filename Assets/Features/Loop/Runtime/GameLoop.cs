﻿using System;
using Cysharp.Threading.Tasks;
using Features.Loop.Abstract;
using Features.Loop.Runtime.States;
using Features.Loop.Sounds.Abstract;
using Features.Tutorial.Runtime;
using Global.Publisher.Abstract.Advertisment;
using Global.Publisher.Abstract.DataStorages;
using Global.Saves;
using Internal.Scopes.Abstract.Instances.Services;
using Internal.Scopes.Abstract.Lifetimes;

namespace Features.Loop.Runtime
{
    public class GameLoop : IScopeLoadAsyncListener, IScopeLifetimeListener, IGameLoop
    {
        public GameLoop(
            IAds ads,
            IDataStorage dataStorage,
            IGameSounds gameSounds,
            GamePlayState gamePlay,
            GameEndState gameEnd,
            TutorialState tutorial)
        {
            _ads = ads;
            _dataStorage = dataStorage;
            _gameSounds = gameSounds;
            _gamePlay = gamePlay;
            _gameEnd = gameEnd;
            _tutorial = tutorial;
        }

        private readonly IAds _ads;
        private readonly IDataStorage _dataStorage;
        private readonly IGameSounds _gameSounds;
        private readonly GamePlayState _gamePlay;
        private readonly GameEndState _gameEnd;
        private readonly TutorialState _tutorial;

        private ILifetime _lifetime;

        public void OnLifetimeCreated(ILifetime lifetime)
        {
            _lifetime = lifetime;
        }

        public async UniTask OnLoadedAsync()
        {
            _gameSounds.StartBackgroundMusic();

            var tutorialSave = await _dataStorage.GetEntry<TutorialSave>();

            if (tutorialSave.IsTutorialPassed == true)
                RunLoop(_gamePlay).Forget();
            else
                RunLoop(_tutorial).Forget();
        }

        private async UniTask RunLoop(ILoopState step)
        {
            while (_lifetime.IsTerminated == false)
            {
                var stepLifetime = _lifetime.CreateChild();
                var next = await step.Enter(stepLifetime);
                stepLifetime.Terminate();

                step = next switch
                {
                    GameStateType.Game => _gamePlay,
                    GameStateType.End => _gameEnd,
                    GameStateType.Tutorial => _tutorial,
                    _ => throw new ArgumentOutOfRangeException()
                };
            }
        }
    }
}