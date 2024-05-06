using Common.DataTypes.Runtime.Structs;
using Cysharp.Threading.Tasks;
using Features.GamePlay.Boards.Abstract.Boards;
using Features.GamePlay.Boards.Abstract.Moves;
using Features.Loop.Sounds.Abstract;
using Internal.Scopes.Abstract.Lifetimes;

namespace Features.GamePlay.Boards.Runtime.Moves
{
    public class BoardMover : IBoardMover
    {
        public BoardMover(IBoardScanner scanner, IBoardLifecycle boardLifecycle, IGameSounds gameSounds)
        {
            _scanner = scanner;
            _boardLifecycle = boardLifecycle;
            _gameSounds = gameSounds;
        }

        private readonly IBoardScanner _scanner;
        private readonly IBoardLifecycle _boardLifecycle;
        private readonly IGameSounds _gameSounds;

        public async UniTask Move(IReadOnlyLifetime lifetime, CoordinateDirection direction)
        {
            _boardLifecycle.OnMoveStarted();

            var blocks = _scanner.GetBlocksSortedByDirection(direction);

            var isAnyBlockUpgraded = false;
            var wasMoved = false;

            foreach (var block in blocks)
            {
                if (block.Value.IsUpgradeStarted == true)
                    continue;

                var targetTile = _scanner.GetTargetTile(block, direction, false);

                if (targetTile == block.Tile)
                    continue;

                wasMoved = true;

                if (targetTile.Block != null)
                    isAnyBlockUpgraded = true;

                block.MoveTo(targetTile);
            }

            if (wasMoved == true)
            {
                if (isAnyBlockUpgraded == true)
                    _gameSounds.PlayBlockCombine();
                else
                    _gameSounds.PlayBlockMove();
            }
        }
    }
}