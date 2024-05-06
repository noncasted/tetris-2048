using Common.DataTypes.Runtime.Reactive;
using Features.GamePlay.Boards.Abstract.Blocks;
using UnityEngine;

namespace Features.GamePlay.Boards.Abstract.Boards
{
    public interface IBoardTile
    {
        Vector2Int BoardPosition { get; }
        Vector2 Position { get; }
        
        IBoardBlock Block { get; }
        IViewableDelegate BlockEntered { get; }
        
        bool IsLocked { get; }
        bool IsTaken { get; }

        void RemoveBlock(IBoardBlock block);
        void SetBlock(IBoardBlock block);

        void Take();
        void Free();
    }
}