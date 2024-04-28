using Common.DataTypes.Runtime.Structs;
using Cysharp.Threading.Tasks;
using GamePlay.BlockSpawners.Abstract;
using GamePlay.Boards.Abstract.Boards;
using GamePlay.Loop.Abstract;
using GamePlay.Loop.Scores.Abstract;
using GamePlay.Save.Abstract;
using Internal.Scopes.Abstract.Lifetimes;

namespace GamePlay.Loop.Runtime
{
    public class GamePlayLoop : IGamePlayLoop
    {
        public GamePlayLoop(IBoard board, IBlockSpawner blockSpawner, IScore score)
        {
            _board = board;
            _blockSpawner = blockSpawner;
            _score = score;
        }

        private readonly IBoard _board;
        private readonly IBlockSpawner _blockSpawner;
        private readonly IScore _score;

        public void Construct(IReadOnlyLifetime lifetime, GameSave save)
        {
            _board.Fill(lifetime, save);
        }

        public async UniTask<GameResult> HandleGame(IReadOnlyLifetime lifetime)
        {
            var boardHandle = await _board.CreateHandle(lifetime);
            _blockSpawner.Start(lifetime);

            var boardResult = await boardHandle.GameCompletion.Task;

            return new GameResult(_score.Current.Value);
        }

        public void PassInput(CoordinateDirection direction)
        {
            _board.OnInput(direction);
        }
    }
}