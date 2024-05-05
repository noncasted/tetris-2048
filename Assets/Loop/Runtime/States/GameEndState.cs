using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GamePlay.Loop.Scores.Abstract;
using GamePlay.Save.Abstract;
using Global.Publisher.Abstract.Advertisment;
using Global.Saves;
using Global.UI.StateMachines.Abstract;
using Internal.Scopes.Abstract.Lifetimes;
using Loop.Abstract;
using Menu.GameEnds.Abstract;
using Menu.Loop.Abstract;
using UnityEngine;

namespace Loop.Runtime.States
{
    public class GameEndState : ILoopState
    {
        public GameEndState(
            IUiStateMachine stateMachine,
            IMenuGameEnd gameEnd,
            IScore score,
            IAds ads,
            IGameSaver gameSaver)
        {
            _stateMachine = stateMachine;
            _gameEnd = gameEnd;
            _score = score;
            _ads = ads;
            _gameSaver = gameSaver;
        }

        private readonly IUiStateMachine _stateMachine;
        private readonly IMenuGameEnd _gameEnd;
        private readonly IScore _score;
        private readonly IAds _ads;
        private readonly IGameSaver _gameSaver;

        public async UniTask<GameStateType> Enter(IReadOnlyLifetime stateLifetime)
        {
            var completion = new UniTaskCompletionSource<GameStateType>();
            stateLifetime.ListenTerminate(() => completion.TrySetCanceled());

            var handle = _stateMachine.EnterAsStack(_stateMachine.Base, _gameEnd);
            _gameEnd.Show(handle, _score.Current.Value);

            _gameEnd.ExitRequested.Listen(stateLifetime, () =>
            {
                handle.Exit();
                completion.TrySetResult(GameStateType.Game);
            });

            var completionResult = await completion.Task;

            await _ads.ShowInterstitial();

            _gameSaver.ForceSave(new List<BoardStateBlock>());

            return completionResult;
        }
    }
}