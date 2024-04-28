using System;
using Common.DataTypes.Runtime.Structs;
using GamePlay.Boards.Abstract.Blocks;
using GamePlay.Boards.Abstract.Boards;
using GamePlay.Boards.Abstract.Factory;
using GamePlay.Boards.Abstract.Moves;
using GamePlay.Boards.Runtime.Blocks.Moving.Actions;
using Global.System.Updaters.Progressions;
using Internal.Scopes.Abstract.Lifetimes;
using Loop.Sounds.Abstract;
using UnityEngine;

namespace GamePlay.Boards.Runtime.Blocks.Moving
{
    public class MovingBlock : MonoBehaviour, IMovingBlockInteractor, IMovingBlock
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private BlockConfig _config;
        [SerializeField] private MovingBlockValue _value;

        private IBoardTile _currentTile;

        private ILifetime _lifetime;
        private IBlockFactory _blockFactory;
        private IBoardScanner _scanner;
        private IMovingBlockAction _currentAction;
        private IProgressionHandle _progressionHandle;
        private IGameSounds _sounds;

        public MovingBlockValue Value => _value;
        public IBoardTile Tile => _currentTile;
        public IReadOnlyLifetime Lifetime => _lifetime;

        public void Construct(
            IGameSounds sounds,
            IProgressionFactory progressionFactory,
            IReadOnlyLifetime lifetime,
            IBoardView view,
            IBlockFactory blockFactory,
            IBoardScanner scanner,
            int value,
            IBoardTile tile)
        {
            _sounds = sounds;
            _scanner = scanner;
            _blockFactory = blockFactory;
            _lifetime = lifetime.CreateChild();
            _value.Construct(value);
            _transform.position = tile.Position;

            _currentTile = tile;
            _currentTile.Take();

            if (_lifetime.IsTerminated == true)
                return;

            _progressionHandle = progressionFactory.CreateHandle();

            view.MoveStarted.Listen(_lifetime, OnBoardMoveStarted);
            _currentAction = PickNextAction();
            _currentAction.Start();
        }

        public void SetTile(IBoardTile tile)
        {
            _currentTile = tile;
        }

        public void OnCurrentActionCompleted()
        {
            if (_lifetime.IsTerminated == true)
                return;
            
            _currentAction = PickNextAction();
            _currentAction.Start();
        }

        public void DestroyBlock()
        {
            _currentTile?.Free();
            _currentAction = null;
            _lifetime.Terminate();
            Destroy(gameObject);
        }

        private void OnBoardMoveStarted()
        {
            _currentAction.Break();

            if (_lifetime.IsTerminated == true)
                return;

            if (_currentTile.IsLocked == false)
            {
                DestroyBlock();
                _blockFactory.CreateStatic(_currentTile.BoardPosition, _value.Number);
                return;
            }

            _currentAction = PickNextAction();
            _currentAction.Start();
        }

        private IMovingBlockAction PickNextAction()
        {
            if (_lifetime.IsTerminated == true)
                throw new Exception();

            var targetTile = _scanner.GetFirstTargetTile(
                _currentTile.BoardPosition,
                _value.Number,
                CoordinateDirection.Up,
                true);

            _currentAction = null;

            if (targetTile.IsLocked == true)
            {
                if (targetTile.IsTaken == true)
                    return CreateWait();

                if (targetTile.Block != null)
                    throw new Exception();

                return CreateMove();
            }

            if (targetTile == _currentTile)
            {
                DestroyBlock();
                _blockFactory.CreateStatic(_currentTile.BoardPosition, _value.Number);
                return new MovingBlockEmptyAction();
            }

            if (targetTile.IsTaken == false)
            {
                if (targetTile.Block != null)
                    throw new Exception();

                return CreateMove();
            }

            if (targetTile.Block == null)
                throw new Exception();

            return CreateUpgrade();

            MovingBlockWaitAction CreateWait()
            {
                return new MovingBlockWaitAction(_lifetime, _progressionHandle, this, _config);
            }

            MovingBlockMoveAction CreateMove()
            {
                var options = new MovingBlockMoveOptions(_currentTile, targetTile, this, _transform, _config);
                return new MovingBlockMoveAction(_lifetime, _progressionHandle, this, options);
            }

            MovingBlockUpgradeAction CreateUpgrade()
            {
                _sounds.PlayBlockCombine();
                var options = new MovingBlockMoveOptions(_currentTile, targetTile, this, _transform, _config);
                return new MovingBlockUpgradeAction(_lifetime, _progressionHandle, this, options);
            }
        }
    }
}