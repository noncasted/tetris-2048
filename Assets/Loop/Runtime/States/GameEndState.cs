using Cysharp.Threading.Tasks;
using GamePlay.Loop.Scores.Abstract;
using Global.UI.StateMachines.Abstract;
using Internal.Scopes.Abstract.Lifetimes;
using Loop.Abstract;
using Menu.GameEnds.Abstract;
using Menu.Loop.Abstract;

namespace Loop.Runtime.States
{
    public class GameEndState : ILoopState
    {
        public GameEndState(
            IUiStateMachine stateMachine,
            IMenuMain main,
            IMenuGameEnd gameEnd,
            IScore score)
        {
            _stateMachine = stateMachine;
            _main = main;
            _gameEnd = gameEnd;
            _score = score;
        }

        private readonly IUiStateMachine _stateMachine;
        private readonly IMenuMain _main;
        private readonly IMenuGameEnd _gameEnd;
        private readonly IScore _score;

        public UniTask<GameStateType> Enter(IReadOnlyLifetime stateLifetime)
        {
            var completion = new UniTaskCompletionSource<GameStateType>();
            stateLifetime.ListenTerminate(() => completion.TrySetCanceled());

            var handle = _stateMachine.EnterAsChild(_main, _gameEnd);
            _gameEnd.Show(handle, _score.Current.Value);
            
            _gameEnd.ExitRequested.Listen(stateLifetime, () =>
            {
                handle.Exit();
                completion.TrySetResult(GameStateType.Game);
            });

            return completion.Task;
        }
    }
}