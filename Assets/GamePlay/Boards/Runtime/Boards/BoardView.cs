using System;
using System.Collections.Generic;
using Common.DataTypes.Runtime.Reactive;
using GamePlay.Boards.Abstract.Blocks;
using GamePlay.Boards.Abstract.Boards;
using UnityEngine;

namespace GamePlay.Boards.Runtime.Boards
{
    [DisallowMultipleComponent]
    public class BoardView : MonoBehaviour, IBoardView, IBoardLifecycle
    {
        [SerializeField] private BoardColumns[] _columns;
        [SerializeField] private BoardTile[] _loseTiles;
        [SerializeField] private Transform _blocksRoot;

        private readonly ViewableDelegate _moveStarted = new();

        private IReadOnlyDictionary<int, IReadOnlyDictionary<int, IBoardTile>> _cachedTiles;
        private IReadOnlyList<IBoardTile> _cachedFlatTiles;

        public Vector2Int Size => new(Tiles.Count, Tiles[0].Count);
        public Transform BlocksRoot => _blocksRoot;
        public IReadOnlyList<IBoardTile> FlatTiles => GetFlatTiles();
        public IReadOnlyList<IBoardTile> LoseTiles => _loseTiles;
        public IReadOnlyDictionary<int, IReadOnlyDictionary<int, IBoardTile>> Tiles => GetTiles();
        public IViewableDelegate MoveStarted => _moveStarted;

        public void OnMoveStarted()
        {
            _moveStarted.Invoke();
        }

        public IReadOnlyList<IBoardBlock> GetCurrentBlocks()
        {
            var tiles = Tiles;
            var blocks = new List<IBoardBlock>();

            foreach (var (_, column) in tiles)
            {
                foreach (var (_, tile) in column)
                {
                    if (tile.IsLocked == true)
                        continue;

                    if (tile.Block != null)
                        blocks.Add(tile.Block);
                }
            }

            return blocks;
        }

        private IReadOnlyList<IBoardTile> GetFlatTiles()
        {
            if (_cachedFlatTiles != null)
                return _cachedFlatTiles;

            var allTiles = GetTiles();
            var flatTiles = new List<IBoardTile>();

            foreach (var (_, column) in allTiles)
            {
                foreach (var (_, tile) in column)
                    flatTiles.Add(tile);
            }

            _cachedFlatTiles = flatTiles;
            return flatTiles;
        }

        private IReadOnlyDictionary<int, IReadOnlyDictionary<int, IBoardTile>> GetTiles()
        {
            if (_cachedTiles != null)
                return _cachedTiles;

            var tiles = new Dictionary<int, IReadOnlyDictionary<int, IBoardTile>>();

            foreach (var column in _columns)
            {
                var dictionary = new Dictionary<int, IBoardTile>();
                tiles.Add(column.Tiles[0].BoardPosition.x, dictionary);

                foreach (var tile in column.Tiles)
                    dictionary.Add(tile.BoardPosition.y, tile);
            }

            _cachedTiles = tiles;

            return tiles;
        }
    }

    [Serializable]
    public class BoardColumns
    {
        [SerializeField] public BoardTile[] Tiles;
    }
}