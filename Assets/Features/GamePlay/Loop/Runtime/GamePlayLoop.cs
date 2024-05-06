using Common.DataTypes.Runtime.Structs;
using Cysharp.Threading.Tasks;
using Features.GamePlay.BlockSpawners.Abstract;
using Features.GamePlay.Boards.Abstract;
using Features.GamePlay.Boards.Abstract.Boards;
using Features.GamePlay.Loop.Abstract;
using Features.GamePlay.Loop.Scores.Abstract;
using Features.Loop.Abstract;
using Global.Saves;
using Internal.Scopes.Abstract.Lifetimes;

namespace Features.GamePlay.Loop.Runtime
{
    public class GamePlayLoop : IGamePlayLoop
    {
        public GamePlayLoop(
            IBoard board,
            IBlockSpawner blockSpawner,
            IScore score,
            GameLoopCheats cheats)
        {
            _board = board;
            _blockSpawner = blockSpawner;
            _score = score;
            _cheats = cheats;
        }

        private readonly IBoard _board;
        private readonly IBlockSpawner _blockSpawner;
        private readonly IScore _score;
        private readonly GameLoopCheats _cheats;

        public void Construct(IReadOnlyLifetime lifetime, GameSave save)
        {
            _board.Fill(lifetime, save);
        }

        public async UniTask<GameResult> HandleGame(IReadOnlyLifetime lifetime)
        {
            var boardHandle = await _board.CreateHandle(lifetime);
            _blockSpawner.Start(lifetime);
            
            _cheats.EndGame.Listen(lifetime, () => boardHandle.GameCompletion.TrySetResult(new BoardResult()));
            lifetime.ListenTerminate(() => boardHandle.GameCompletion.TrySetCanceled());
            
            var boardResult = await boardHandle.GameCompletion.Task;

            return new GameResult(_score.Current.Value);
        }

        public void PassInput(CoordinateDirection direction)
        {
            _board.OnInput(direction);
        }
    }
}