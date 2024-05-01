using System.Collections.Generic;
using Common.DataTypes.Runtime.Structs;
using GamePlay.Boards.Abstract.Blocks;
using GamePlay.Boards.Abstract.Boards;
using GamePlay.Save.Abstract;
using Global.Saves;
using UnityEngine;

namespace GamePlay.Boards.Abstract.Moves
{
    public interface IBoardScanner
    {
        bool IsMovePossible();
        List<IBoardBlock> GetBlocksSortedByDirection(CoordinateDirection direction);

        IBoardTile GetTargetTile(IBoardBlock block, CoordinateDirection direction, bool allowBlockedTiles);

        IBoardTile GetFirstTargetTile(
            Vector2Int boardPosition,
            int blockValue,
            CoordinateDirection direction,
            bool allowBlockedTiles);

        int GetScore();

        List<BoardStateBlock> GetState();
    }
}