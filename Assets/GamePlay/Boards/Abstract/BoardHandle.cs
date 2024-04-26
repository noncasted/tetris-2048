using Cysharp.Threading.Tasks;

namespace GamePlay.Boards.Abstract
{
    public class BoardHandle
    {
        public BoardHandle(UniTaskCompletionSource<BoardResult> gameCompletion)
        {
            GameCompletion = gameCompletion;
        }

        public readonly UniTaskCompletionSource<BoardResult> GameCompletion;
    }
}