using GamePlay.Boards.Abstract.Boards;

namespace GamePlay.Boards.Runtime.Blocks.Static.Actions
{
    public interface IStaticBlockInteractor
    {
        void SetTile(IBoardTile tile);
        void DestroyBlock();
    }
}