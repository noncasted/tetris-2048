using System.Collections.Generic;
using System.Linq;
using Common.DataTypes.Runtime.Structs;
using Cysharp.Threading.Tasks;
using GamePlay.Boards.Abstract;
using GamePlay.Boards.Abstract.Boards;
using GamePlay.Boards.Abstract.Factory;
using GamePlay.Boards.Abstract.Moves;
using GamePlay.Boards.Runtime.Blocks.Moving;
using GamePlay.Loop.Scores.Abstract;
using GamePlay.Save.Abstract;
using Global.Saves;
using Internal.Scopes.Abstract.Lifetimes;
using Loop.Abstract;
using UnityEngine;

namespace GamePlay.Boards.Runtime.Boards
{
    public class Board : IBoard
    {
        public Board(
            IBoardView view,
            IBoardLifecycle lifecycle,
            IBlockFactory blockFactory,
            IBoardMover mover,
            IBoardScanner scanner,
            IGameState gameState,
            IScore score,
            IGameSaver gameSaver)
        {
            _view = view;
            _lifecycle = lifecycle;
            _blockFactory = blockFactory;
            _mover = mover;
            _scanner = scanner;
            _gameState = gameState;
            _score = score;
            _gameSaver = gameSaver;
        }
        
        private const int _startBlocksAmount = 4;

        private readonly IBoardView _view;
        private readonly IBoardLifecycle _lifecycle;
        private readonly IBlockFactory _blockFactory;
        private readonly IBoardMover _mover;
        private readonly IBoardScanner _scanner;
        private readonly IGameState _gameState;
        private readonly IScore _score;
        private readonly IGameSaver _gameSaver;

        private ILifetime _moveLifetime;
        private IReadOnlyLifetime _scopeLifetime;
        private List<IBoardTile> _loseTiles;
        private UniTaskCompletionSource<BoardResult> _completion;

        public void Fill(IReadOnlyLifetime lifetime, GameSave save)
        {
            _blockFactory.AddGamePlayLifetime(lifetime);
            ClearBoard();

            var saveTiles = save.Tiles;

            if (saveTiles.Count == 0)
                saveTiles = CreateNewBoard();

            _loseTiles = new List<IBoardTile>();

            foreach (var (_, column) in _view.Tiles)
            {
                var minIndex = column.Keys.Min();
                _loseTiles.Add(column[minIndex]);
            }

            foreach (var stateBlock in saveTiles)
                _blockFactory.CreateStatic(stateBlock.Position, stateBlock.Value);

            _scopeLifetime = lifetime;

            var score = _scanner.GetScore();
            _score.SetCurrent(score);

            _gameSaver.ForceSave(saveTiles);
        }

        public async UniTask<BoardHandle> CreateHandle(IReadOnlyLifetime lifetime)
        {
            _completion = new UniTaskCompletionSource<BoardResult>();

            foreach (var loseTile in _view.LoseTiles)
                loseTile.BlockEntered.Listen(lifetime, OnLoseTileEntered);
            
            return new BoardHandle(_completion);
        }

        public void OnInput(CoordinateDirection direction)
        {
            if (_gameState.State.Value != GameStateType.Game && _gameState.State.Value != GameStateType.Tutorial)
                return;

            _moveLifetime?.Terminate();
            _moveLifetime = _scopeLifetime.CreateChild();

            _mover.Move(_moveLifetime, direction).Forget();

            var score = _scanner.GetScore();
            _score.SetCurrent(score);

            var tiles = _scanner.GetState();
            _gameSaver.SaveTemporary(tiles);
        }

        private void ClearBoard()
        {
            _lifecycle.OnBoardClear();
            var currentBlocks = _view.GetCurrentBlocks();

            foreach (var block in currentBlocks)
            {
                block.Tile.RemoveBlock(block);
                block.DestroyBlock();
            }

            foreach (var tile in _view.FlatTiles)
                tile.Free();

            var movingBlocks =
                Object.FindObjectsByType<MovingBlock>(FindObjectsInactive.Include, FindObjectsSortMode.None);

            foreach (var block in movingBlocks)
                block.DestroyBlock();
        }

        private void OnLoseTileEntered()
        {
            if (IsAllLoseTilesTaken() == false)
                return;

            if (_scanner.IsMovePossible() == true)
                return;

            _completion.TrySetResult(new BoardResult());

            return;

            bool IsAllLoseTilesTaken()
            {
                foreach (var loseTile in _loseTiles)
                {
                    if (loseTile.IsTaken == false)
                        return false;
                }

                return true;
            }
        }

        private List<BoardStateBlock> CreateNewBoard()
        {
            var tiles = _view.Tiles;
            var allTiles = tiles
                .SelectMany(column => column.Value)
                .Where(t => t.Value.IsLocked == false)
                .Select(t => t.Value).ToList();

            var saveTiles = new List<BoardStateBlock>();

            for (var i = 0; i < _startBlocksAmount; i++)
            {
                var tile = allTiles[Random.Range(0, tiles.Count)];
                allTiles.Remove(tile);
                saveTiles.Add(new BoardStateBlock(tile.BoardPosition, 2));
            }

            return saveTiles;
        }
    }
}