using GamePlay.Boards.Abstract.Boards;

namespace GamePlay.Boards.Runtime.Blocks.Moving.Actions
{
    public interface IMovingBlockInteractor
    {
        void SetTile(IBoardTile tile);
        void OnCurrentActionCompleted();
        void DestroyBlock();
    }
}