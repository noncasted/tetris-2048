using GamePlay.Boards.Abstract.Blocks;
using GamePlay.Boards.Abstract.Boards;
using GamePlay.Boards.Abstract.Factory;
using GamePlay.Boards.Abstract.Moves;
using Global.System.Updaters.Progressions;
using Internal.Scopes.Abstract.Lifetimes;
using Loop.Sounds.Abstract;
using UnityEngine;

namespace GamePlay.Boards.Runtime.Factory
{
    public class BlockFactory : IBlockFactory
    {
        public BlockFactory(
            IProgressionFactory progressionFactory,
            IBoardView view,
            IBoardScanner scanner,
            IBoardLifecycle boardLifecycle,
            IGameSounds sounds,
            BlockFactoryConfig config)
        {
            _progressionFactory = progressionFactory;
            _view = view;
            _scanner = scanner;
            _boardLifecycle = boardLifecycle;
            _sounds = sounds;
            _config = config;
        }

        private readonly IProgressionFactory _progressionFactory;
        private readonly IBoardView _view;
        private readonly IBoardScanner _scanner;
        private readonly IBoardLifecycle _boardLifecycle;
        private readonly IGameSounds _sounds;
        private readonly BlockFactoryConfig _config;

        private IReadOnlyLifetime _lifetime;

        public void AddGamePlayLifetime(IReadOnlyLifetime lifetime)
        {
            _lifetime = lifetime;
        }

        public void CreateStatic(Vector2Int position, int value)
        {
            var tile = _view.Tiles[position.x][position.y];
            var block = Object.Instantiate(_config.Static, _view.BlocksRoot);
            block.name = $"Block_{position.x}_{position.y}";

            block.Construct(_lifetime, _progressionFactory, tile, _boardLifecycle, value);
            tile.SetBlock(block);
        }

        public IMovingBlock CreateMoving(IReadOnlyLifetime lifetime, IBoardTile tile, int value)
        {
            var block = Object.Instantiate(_config.Moving, _view.BlocksRoot);
            block.name = $"Block_{tile.BoardPosition.x}_{tile.BoardPosition.y}";
            block.Construct(_sounds, _progressionFactory, lifetime, _view, this, _scanner, value, tile);
            return block;
        }
    }
}