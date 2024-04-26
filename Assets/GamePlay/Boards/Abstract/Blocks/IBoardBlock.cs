using GamePlay.Boards.Abstract.Boards;

namespace GamePlay.Boards.Abstract.Blocks
{
    public interface IBoardBlock
    {
        IBoardBlockValue Value { get; }
        IBoardTile Tile { get; }

        void MoveTo(IBoardTile target);
        void DestroyBlock();
    }
}