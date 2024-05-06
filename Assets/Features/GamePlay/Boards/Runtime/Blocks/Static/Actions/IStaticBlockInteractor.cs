using Features.GamePlay.Boards.Abstract.Boards;

namespace Features.GamePlay.Boards.Runtime.Blocks.Static.Actions
{
    public interface IStaticBlockInteractor
    {
        void SetTile(IBoardTile tile);
        void DestroyBlock();
    }
}