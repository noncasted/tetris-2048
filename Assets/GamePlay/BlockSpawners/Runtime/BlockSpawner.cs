﻿using System.Collections.Generic;
using System.Linq;
using GamePlay.BlockSpawners.Abstract;
using GamePlay.Boards.Abstract.Boards;
using GamePlay.Boards.Abstract.Factory;
using Global.System.Updaters.Abstract;
using Internal.Scopes.Abstract.Lifetimes;
using UnityEngine;

namespace GamePlay.BlockSpawners.Runtime
{
    public class BlockSpawner : IBlockSpawner, IUpdatable
    {
        public BlockSpawner(
            IUpdater updater,
            IBoardView view,
            IBlockFactory factory,
            BlockSpawnerConfig config)
        {
            _factory = factory;
            _updater = updater;
            _view = view;
            _config = config;
        }

        private readonly IBlockFactory _factory;
        private readonly IUpdater _updater;
        private readonly IBoardView _view;
        private readonly BlockSpawnerConfig _config;

        private float _time;
        private IReadOnlyLifetime _lifetime;
        private IReadOnlyList<IBoardTile> _spawnTiles;

        public void Start(IReadOnlyLifetime lifetime)
        {
            _lifetime = lifetime;

            var spawnTiles = new List<IBoardTile>();

            foreach (var (_, column) in _view.Tiles)
            {
                var minIndex = column.Keys.Min();
                spawnTiles.Add(column[minIndex]);
            }

            _spawnTiles = spawnTiles;

            _updater.Add(lifetime, this);
            _time = _config.SpawnRate / 2f;
        }

        public void OnUpdate(float delta)
        {
            if (_lifetime.IsTerminated == true)
                return;
            
            _time += delta;

            if (_time < _config.SpawnRate)
                return;

            _time = 0f;
            Spawn();
        }

        private void Spawn()
        {
            var value = _config.ValuesProbability[Random.Range(0, _config.ValuesProbability.Count)];
            var tiles = new List<IBoardTile>(_spawnTiles);

            while (tiles.Count != 0)
            {
                var randomIndex = Random.Range(0, tiles.Count);
                var tile = tiles[randomIndex];

                if (tile.IsTaken == true)
                {
                    tiles.RemoveAt(randomIndex);
                    continue;
                }

                _factory.CreateMoving(_lifetime, tile, value);
                return;
            }
        }
    }
}