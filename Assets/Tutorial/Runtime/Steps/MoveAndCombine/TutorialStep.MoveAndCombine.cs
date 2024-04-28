using Common.DataTypes.Runtime.Structs;
using Cysharp.Threading.Tasks;
using GamePlay.Boards.Abstract.Boards;
using GamePlay.Input.Abstract;
using Internal.Scopes.Abstract.Lifetimes;

namespace Tutorial.Runtime.Steps.MoveAndCombine
{
    public class TutorialStep_MoveAndCombine : ITutorialStep
    {
        public TutorialStep_MoveAndCombine(
            IGameInput input,
            IBoard board,
            TutorialStep_MoveAndCombine_UI ui)
        {
            _input = input;
            _board = board;
            _ui = ui;
        }

        private readonly IGameInput _input;
        private readonly IBoard _board;
        private readonly TutorialStep_MoveAndCombine_UI _ui;

        public async UniTask Handle(IReadOnlyLifetime stepLifetime)
        {
            _ui.Enter(stepLifetime);
            var direction = CoordinateDirection.Right;
            _input.Swipe.Listen(stepLifetime, input => direction = input);
            
            await UniTask.WaitUntil(
                () => direction == CoordinateDirection.Left,
                PlayerLoopTiming.Update,
                stepLifetime.Token);
            
            _board.OnInput(CoordinateDirection.Left);
        }
    }
}