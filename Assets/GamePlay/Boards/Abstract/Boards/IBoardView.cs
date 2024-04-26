﻿using System.Collections.Generic;
using Common.DataTypes.Runtime.Reactive;
using GamePlay.Boards.Abstract.Blocks;
using UnityEngine;

namespace GamePlay.Boards.Abstract.Boards
{
    public interface IBoardView
    {
        Vector2Int Size { get; }
        Transform BlocksRoot { get; }
        
        IReadOnlyDictionary<int, IReadOnlyDictionary<int, IBoardTile>> Tiles { get; }
        IReadOnlyList<IBoardTile> FlatTiles { get; }
        IReadOnlyList<IBoardTile> LoseTiles { get; }
        
        IViewableDelegate MoveStarted { get; }

        IReadOnlyList<IBoardBlock> GetCurrentBlocks();
    }
}