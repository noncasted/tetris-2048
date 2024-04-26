using Common.DataTypes.Runtime.Reactive;
using GamePlay.Boards.Abstract.Blocks;
using UnityEngine;

namespace GamePlay.Boards.Abstract.Boards
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