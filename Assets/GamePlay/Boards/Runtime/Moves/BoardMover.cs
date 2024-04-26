using Common.DataTypes.Runtime.Structs;
using Cysharp.Threading.Tasks;
using GamePlay.Boards.Abstract.Boards;
using GamePlay.Boards.Abstract.Moves;
using Internal.Scopes.Abstract.Lifetimes;

namespace GamePlay.Boards.Runtime.Moves
{
    public class BoardMover : IBoardMover
    {
        public BoardMover(IBoardScanner scanner, IBoardLifecycle boardLifecycle)
        {
            _scanner = scanner;
            _boardLifecycle = boardLifecycle;
        }

        private readonly IBoardScanner _scanner;
        private readonly IBoardLifecycle _boardLifecycle;

        public async UniTask Move(IReadOnlyLifetime lifetime, CoordinateDirection direction)
        {
            _boardLifecycle.OnMoveStarted();

            var blocks = _scanner.GetBlocksSortedByDirection(direction);

            foreach (var block in blocks)
            {
                if (block.Value.IsUpgradeStarted == true)
                    continue;

                var targetTile = _scanner.GetTargetTile(block, direction, false);

                if (targetTile == block.Tile)
                    continue;

                block.MoveTo(targetTile);
            }
        }
    }
}