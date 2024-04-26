using System;
using System.Collections.Generic;
using System.Linq;
using Common.DataTypes.Runtime.Structs;
using GamePlay.Boards.Abstract.Blocks;
using GamePlay.Boards.Abstract.Boards;
using GamePlay.Boards.Abstract.Moves;
using GamePlay.Save.Abstract;
using UnityEngine;

namespace GamePlay.Boards.Runtime.Moves
{
    public class BoardScanner : IBoardScanner
    {
        public BoardScanner(IBoardView view)
        {
            _view = view;
        }

        private readonly IBoardView _view;

        private readonly List<CoordinateDirection> _allDirections = new()
        {
            CoordinateDirection.Up,
            CoordinateDirection.Right,
            CoordinateDirection.Down,
            CoordinateDirection.Left
        };

        public bool IsMovePossible()
        {
            var blocks = _view.GetCurrentBlocks();

            foreach (var block in blocks)
            {
                foreach (var direction in _allDirections)
                {
                    var targetTile = GetTargetTile(block, direction, false);

                    if (targetTile == block.Tile)
                        continue;

                    return true;
                }
            }

            return false;
        }

        public IBoardTile GetTargetTile(
            IBoardBlock block,
            CoordinateDirection direction,
            bool allowBlockedTiles)
        {
            var boardPosition = block.Tile.BoardPosition;
            var wayDirection = GetWayDirection(direction);
            boardPosition += wayDirection;

            while (IsInRange(boardPosition, allowBlockedTiles))
            {
                var tile = GetTile(boardPosition);

                if (tile.IsTaken == false)
                {
                    boardPosition += wayDirection;
                    continue;
                }

                if (tile.Block == null)
                    return GetTile(boardPosition - wayDirection);

                if (tile.Block.Value.CanBeMergedWith(block.Value.Number) == true)
                    return tile;

                return GetTile(boardPosition - wayDirection);
            }

            return GetTile(boardPosition - wayDirection);
        }

        public IBoardTile GetFirstTargetTile(
            Vector2Int boardPosition,
            int blockValue,
            CoordinateDirection direction,
            bool allowBlockedTiles)
        {
            var startTile = GetTile(boardPosition);
            var wayDirection = GetWayDirection(direction);
            boardPosition += wayDirection;

            if (IsInRange(boardPosition, allowBlockedTiles) == true)
            {
                var tile = GetTile(boardPosition);

                if (tile.IsTaken == false)
                    return tile;

                if (tile.Block == null)
                    return startTile;

                if (tile.Block.Value.CanBeMergedWith(blockValue) == true)
                    return tile;

                return startTile;
            }

            return startTile;
        }

        public int GetScore()
        {
            var score = 0;

            foreach (var tile in _view.FlatTiles)
            {
                if (tile.Block == null)
                    continue;

                score += tile.Block.Value.Number;
            }

            return score;
        }

        public List<BoardStateBlock> GetState()
        {
            var blocks = new List<BoardStateBlock>();

            foreach (var tile in _view.FlatTiles)
            {
                if (tile.Block == null)
                    continue;

                blocks.Add(new BoardStateBlock(tile.BoardPosition, tile.Block.Value.Number));
            }

            return blocks;
        }

        public List<IBoardBlock> GetBlocksSortedByDirection(CoordinateDirection direction)
        {
            var allBlocks = _view.GetCurrentBlocks();

            return direction switch
            {
                CoordinateDirection.Up => allBlocks.OrderBy(t => t.Tile.Position.y).ToList(),
                CoordinateDirection.Right => allBlocks.OrderByDescending(t => t.Tile.Position.x).ToList(),
                CoordinateDirection.Down => allBlocks.OrderByDescending(t => t.Tile.Position.y).ToList(),
                CoordinateDirection.Left => allBlocks.OrderBy(t => t.Tile.Position.x).ToList(),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private IBoardTile GetTile(Vector2Int boardPosition)
        {
            return _view.Tiles[boardPosition.x][boardPosition.y];
        }

        private bool IsInRange(Vector2Int boardPosition, bool isLockedAllowed)
        {
            if (_view.Tiles.TryGetValue(boardPosition.x, out var column) == false)
                return false;

            if (column.TryGetValue(boardPosition.y, out var tile) == false)
                return false;

            if (tile.IsLocked == false)
                return true;

            if (isLockedAllowed == true)
                return true;

            return false;
        }

        private Vector2Int GetWayDirection(CoordinateDirection direction)
        {
            return direction switch
            {
                CoordinateDirection.Up => new Vector2Int(0, 1),
                CoordinateDirection.Right => new Vector2Int(1, 0),
                CoordinateDirection.Down => new Vector2Int(0, -1),
                CoordinateDirection.Left => new Vector2Int(-1, 0),
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
            };
        }
    }
}