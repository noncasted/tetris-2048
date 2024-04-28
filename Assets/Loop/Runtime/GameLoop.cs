using System;
using Cysharp.Threading.Tasks;
using GamePlay.Save.Abstract;
using Global.Publisher.Abstract.DataStorages;
using Internal.Scopes.Abstract.Instances.Services;
using Internal.Scopes.Abstract.Lifetimes;
using Loop.Abstract;
using Loop.Runtime.States;
using Tutorial.Runtime;

namespace Loop.Runtime
{
    public class GameLoop : IScopeLoadAsyncListener, IScopeLifetimeListener, IGameLoop
    {
        public GameLoop(
            IDataStorage dataStorage,
            GamePlayState gamePlay,
            GameEndState gameEnd,
            TutorialState tutorial)
        {
            _dataStorage = dataStorage;
            _gamePlay = gamePlay;
            _gameEnd = gameEnd;
            _tutorial = tutorial;
        }

        private readonly IDataStorage _dataStorage;
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