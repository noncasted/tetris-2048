using System.Linq;
using Common.DataTypes.Runtime.Structs;
using Cysharp.Threading.Tasks;
using GamePlay.Boards.Abstract.Boards;
using GamePlay.Boards.Abstract.Factory;
using GamePlay.Input.Abstract;
using Internal.Scopes.Abstract.Lifetimes;

namespace Tutorial.Runtime.Steps.CombineWithFall
{
    public class TutorialStep_CombineWithFall : ITutorialStep
    {
        public TutorialStep_CombineWithFall(
            IBoard board,
            IGameInput input,
            IBlockFactory factory,
            IBoardView boardView,
            TutorialStep_CombineWithFall_UI ui)
        {
            _board = board;
            _input = input;
            _factory = factory;
            _boardView = boardView;
            _ui = ui;
        }

        private readonly IBoard _board;
        private readonly IGameInput _input;
        private readonly IBlockFactory _factory;
        private readonly IBoardView _boardView;
        private readonly TutorialStep_CombineWithFall_UI _ui;

        public async UniTask Handle(IReadOnlyLifetime stepLifetime)
        {
            var tile = _boardView.LoseTiles.Last();
            _ui.ShowFall();
            _ui.Enter(stepLifetime);

            await UniTask.Delay(1000);
            var block = _factory.CreateMoving(stepLifetime, tile, 4); 
            await UniTask.WaitUntil(() => block.Lifetime.IsTerminated == true);
            _ui.ShowCombine();
            
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